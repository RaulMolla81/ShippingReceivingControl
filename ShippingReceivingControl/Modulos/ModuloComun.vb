Imports System.Data.SqlClient
Imports System.Data
Imports System.Xml
Imports Microsoft.Win32
Imports System.io

Module ModuloComun


    Public NombreUsuario As String
    Public CodUsuario As String

    Public Declare Sub SetBeamHold Lib "SharpEx.dll" (ByVal Enable As Boolean)


    Public Function obtenerfechaservidor() As Date
        Dim ds As DataSet
        If Not ConsultaSQL("SELECT getdate() as Fecha_Hora", ds) Then Exit Function
        If ds.Tables(0).Rows.Count > 0 Then
            obtenerfechaservidor = ds.Tables(0).Rows(0).Item("Fecha_Hora")
        End If
    End Function

    Public Sub IniciaTransaccion(ByRef Con As SqlConnection, ByRef Transaccion As SqlTransaction)
        Transaccion = Con.BeginTransaction(IsolationLevel.ReadCommitted, "Transaccion")
    End Sub
    Public Function EjecutaSQLTransaccion(ByVal Cadena As String, ByRef Transaccion As SqlTransaction) As String
        Dim Escribir As SqlCommand
        Try
            'EJECUTA EL COMANDO SQL
            Escribir = New SqlCommand(Cadena, Conexion, Transaccion)
            Escribir.ExecuteNonQuery()
            Return ""
        Catch ex As SqlException
            Try
                Transaccion.Rollback("Transaccion")
            Catch ex2 As Exception

            End Try
            Return ex.Message
        End Try
    End Function

    Public Function CalculaDigitoEAN13(ByVal codigo As String) As String
        Dim c1, c2 As String
        Dim nX, nX2 As Integer
        CalculaDigitoEAN13 = " "  ' de momento en blanco
        codigo = Trim(codigo)              ' suprimir blancos por si acaso
        If Len(codigo) <> 7 And Len(codigo) <> 12 Then Exit Function ' solo admite 7 o 12 digitos
        c1 = "131313131313"
        nX2 = 0
        For nX = 1 To Len(codigo)
            nX2 = nX2 + Val(Mid$(codigo, nX, 1)) * Val(Mid$(c1, nX, 1))
        Next
        nX = Val(Microsoft.VisualBasic.Right(Str(nX2), 1))    ' Extraer el valor de la derecha
        If nX > 0 Then nX = 10 - nX ' restarlo de 10 si no es ya 0
        CalculaDigitoEAN13 = Trim(Str(nX))
    End Function

    Public Sub FormatoTextBox(ByRef txtTextBox As String, ByVal Formato As Integer)
        ' Da formato a un textbox consrvando la posición del cursor y modificando
        ' su contenido
        ' Esta función fue diseñada para su uso en un evento Change de un textbox
        ' txtTEXTBOX es el TextBox que se va a modificar
        ' Formato puede se cualquiera de las siguientes constantes
        ' Formato :
        '0- Mayusculas

        '1- Numérico (Entero)
        '2- Numerico + Coma ","
        '3- Numérico + punto "."
        '4- Decimal (Reemplaza Comas por Puntos "," -> ".") (Max 1 coma o punto)
        '5- Decimal (Reemplaza Puntos por Comas "." -> ",") (Max 1 coma o punto)
        '6- Decimal negativo (Reemplaza Puntos por Comas "." -> ",") (Max 1 coma o punto)
        '   20 - 30 > Formatos de fecha y/o hora
        '20- Sólo Fecha (DD/MM/[YY]yy) (Donde DD y MM están limitados a 31 y 12 respectivamente)
        '(Si se incluye [YY] éste deberá ser entre 19 y 21)

        '   50 - 60 > Formato especial:
        '51- Formato de Polos/Tension(V)
        '52- Cambia todos los corchetes "[]" por parentesis "()"
        '53- Numérico + "/" (Cambia espacios por "/")
        '54- Para instrucción SQL: Pasa el texto a mayúsculas, permite sólo Números, letras y ,.()/_;
        '55- Texto para instrucción SQL: Evita caracteres que podrían crear un error en una consulta SQL
        '56- (Mayúsculas) Texto para instrucción SQL: Evita caracteres que podrían crear un error en una consulta SQL
        'Los valores 4 y 5 son los mejores para la representación de un número decimal
        'Solo admiten una coma o punto, y hacen que sea más simple cambiar la posición
        ' del punto o coma
        Dim Cursor As Integer
        Dim CursorIni As Integer
        Dim caracter As String
        Dim NCh As Integer
        Static Bloqueo As Boolean
        If txtTextBox = "" Then Exit Sub
        On Error Resume Next
        Err.Clear()

        If VarType(txtTextBox) <> vbString Then Exit Sub
        If Bloqueo = True Then Exit Sub
        Bloqueo = True

        NCh = Len(txtTextBox)
        Cursor = txtTextBox.Length
        CursorIni = 1
        Select Case Formato
            Case 0 'Pasar a mayusculas
                txtTextBox = Replace(UCase(txtTextBox), "'", "´")
            Case 1 'Permitir Números
                txtTextBox = PermitirCaracteres(txtTextBox, "1234567890", CursorIni, Cursor)
            Case 2 'Permitir Números y comas
                txtTextBox = PermitirCaracteres(txtTextBox, "1234567890,", CursorIni, Cursor)
            Case 3 'Permitir Números y puntos
                txtTextBox = PermitirCaracteres(txtTextBox, "1234567890.", CursorIni, Cursor)
            Case 4 'Numeros decimales con punto
                txtTextBox = PermitirCaracteres(txtTextBox, "1234567890,.", CursorIni, Cursor)
                txtTextBox = TextoDecimal(txtTextBox, Cursor)
                txtTextBox = Replace(txtTextBox, ",", ".")
            Case 5 'Numeros decimales con coma
                txtTextBox = PermitirCaracteres(txtTextBox, "1234567890,.", CursorIni, Cursor)
                'If Mid(txtTEXTBOX.Text, 1, 1) = "," Then NCh = NCh - 1
                txtTextBox = Replace(txtTextBox, ".", ",")
            Case 6 'NUMEROS DECIMALES CON COMA Y SIGNO NEGATIVO (-)
                If Len(txtTextBox) > 1 Then
                    txtTextBox = PermitirCaracteres(Mid(txtTextBox, 1, 1), "1234567890,.-", CursorIni, Cursor) & PermitirCaracteres(Mid(txtTextBox, 2), "1234567890,.", CursorIni, Cursor)
                    'If Mid(txtTEXTBOX.Text, 1, 1) = "," Then NCh = NCh - 1
                    If Mid(txtTextBox, 1, 1) = "-" Then
                        txtTextBox = "-" & TextoDecimal(Mid(txtTextBox, 2), CursorIni)
                    Else
                        txtTextBox = TextoDecimal(txtTextBox, CursorIni)
                    End If
                Else
                    txtTextBox = PermitirCaracteres(Mid(txtTextBox, 1, 1), "1234567890,.-", CursorIni, Cursor)
                    'If Mid(txtTEXTBOX.Text, 1, 1) = "," Then NCh = NCh - 1
                    If Mid(txtTextBox, 1, 1) <> "-" Then
                        txtTextBox = TextoDecimal(txtTextBox, CursorIni)
                    End If
                End If
            Case 7  'NUMEROS ENTEROS CON SIGNO NEGATIVO (-)
                If Len(txtTextBox) > 1 Then
                    txtTextBox = PermitirCaracteres(Mid(txtTextBox, 1, 1), "1234567890-", CursorIni, Cursor) & PermitirCaracteres(Mid(txtTextBox, 2), "1234567890,.", CursorIni, Cursor)
                    If Mid(txtTextBox, 1, 1) = "-" Then
                        txtTextBox = "-" & TextoDecimal(Mid(txtTextBox, 2), CursorIni)
                    Else
                        txtTextBox = TextoDecimal(txtTextBox, CursorIni)
                    End If
                Else
                    txtTextBox = PermitirCaracteres(Mid(txtTextBox, 1, 1), "1234567890-", CursorIni, Cursor)
                    'If Mid(txtTEXTBOX.Text, 1, 1) = "," Then NCh = NCh - 1
                    If Mid(txtTextBox, 1, 1) <> "-" Then
                        txtTextBox = TextoDecimal(txtTextBox, CursorIni)
                    End If
                End If
                'Case 20 'Sólo Fecha (DD/MM/[YY]yy) (Donde DD y MM están limitados a 31 y 12 respectivamente)
                '    '(Si se incluye [YY] éste deberá ser entre 19 y 21)
                '    Dim DD As Integer, MM As Integer
                '    txtTextBox.Text = Replace(txtTextBox.Text, "-", "/")
                '    txtTextBox.Text = Replace(txtTextBox.Text, ".", "/")
                '    If Len(txtTextBox.Text) > 4 Then
                '        DD = Val(Left(txtTextBox.Text, 2))
                '        If DD < 1 Then DD = 1
                '        If DD > 31 Then DD = 31
                '        txtTextBox.Text = Format(DD, "00") + Mid(txtTextBox.Text, 3)

                '        DD = Val(Left(txtTextBox.Text, 2))
                '        MM = Val(Mid(txtTextBox.Text, 4, 2))
                '        If DD < 1 Then DD = 1
                '        If DD > 31 Then DD = 31

                '        If MM < 1 Then MM = 1
                '        If MM > 12 Then MM = 12

                '        If Mid(txtTextBox.Text, 5, 1) = "/" Then
                '            txtTextBox.Text = Format(DD, "00") + "/" + Format(MM, "00") + "/" + Mid(txtTextBox.Text, 6)
                '        Else
                '            txtTextBox.Text = Format(DD, "00") + "/" + Format(MM, "00") + Mid(txtTextBox.Text, 6)
                '        End If
                '    ElseIf Len(txtTextBox.Text) > 1 Then
                '        DD = Val(Left(txtTextBox.Text, 2))
                '        If DD < 1 Then DD = 1
                '        If DD > 31 Then DD = 31
                '        If Mid(txtTextBox.Text, 2, 1) = "/" Then
                '            txtTextBox.Text = Format(DD, "00") + "/" + Mid(txtTextBox.Text, 3)
                '        Else
                '            txtTextBox.Text = Format(DD, "00") + Mid(txtTextBox.Text, 3)
                '        End If
                '    End If
                '    If Cursor < 8 Then
                '        Select Case Cursor
                '            Case 0
                '                'txtTextBox.Text = PermitirCaracteres(txtTextBox.Text, "/", Cursor, Cursor)
                '            Case 1
                '                txtTextBox.Text = PermitirCaracteres(txtTextBox.Text, "1234567890", Cursor, Cursor)
                '            Case 2
                '                txtTextBox.Text = PermitirCaracteres(txtTextBox.Text, "1234567890/", Cursor, Cursor)
                '            Case Else
                '                If Mid(txtTextBox.Text, Cursor - 1, 1) = "/" Then
                '                    txtTextBox.Text = PermitirCaracteres(txtTextBox.Text, "1234567890", Cursor, Cursor)
                '                Else
                '                    txtTextBox.Text = PermitirCaracteres(txtTextBox.Text, "1234567890/", Cursor, Cursor)
                '                    If Mid(txtTextBox.Text, Cursor - 2, 1) <> "/" Then
                '                        If Mid(txtTextBox.Text, Cursor, 1) <> "/" Then
                '                            txtTextBox.Text = Left(txtTextBox.Text, Cursor - 1) + "/" + Mid(txtTextBox.Text, Cursor)
                '                        End If
                '                    End If
                '                End If
                '        End Select
                '    Else

                '        txtTextBox.Text = Left(PermitirCaracteres(txtTextBox.Text, "1234567890", Cursor, Cursor), 10)
                '    End If

                '    ' Formatos especiales
                'Case 52 'Cambiar corchetes por parentesis
                '    txtTextBox.Text = Replace(txtTextBox.Text, "[", "(")
                '    txtTextBox.Text = Replace(txtTextBox.Text, "]", ")")
                'Case 53 'Cambiar corchetes por parentesis
                '    If ContarCaracter(txtTextBox.Text, "/") > 1 Then
                '        txtTextBox.Text = PermitirCaracteres(txtTextBox.Text, "1234567890", Cursor, Cursor)
                '    Else
                '        txtTextBox.Text = PermitirCaracteres(txtTextBox.Text, "1234567890/ ", Cursor, Cursor)
                '    End If
                '    txtTextBox.Text = Replace(txtTextBox.Text, " ", "/")
                'Case 54
                '    txtTextBox.Text = UCase(txtTextBox.Text)
                '    txtTextBox.Text = PermitirCaracteres(txtTextBox.Text, "1234567890QWERTYUIOPASDFGHJKLÑZXCVBNMÇ,.(){}\/;%", CursorIni, Cursor)
            Case 55
                txtTextBox = Replace(txtTextBox, "'", "´")
                txtTextBox = NoPermitirCaracteres(txtTextBox, "'=_", CursorIni, Cursor)
            Case 56
                txtTextBox = Replace(UCase(txtTextBox), "'", "´")
                txtTextBox = NoPermitirCaracteres(txtTextBox, "'=_", CursorIni, Cursor)
        End Select

        Cursor = Cursor - NCh + Len(txtTextBox) 'Desplazamiento del cursor que se produce
        If Cursor < 0 Then Cursor = 0 'al modificar el contenido del edit: LongitudNueva-longitudAntigua
        'txtTextBox.SelectionStart = Cursor
        'txtTextBox.SelectionLength = 0
        Bloqueo = False
        If Err.Number <> 0 Then
            MsgBox("[FormatoTextBox] Hubo un error durante la operación : " + vbCrLf + Err.Description, vbCritical)
            Err.Clear()
        End If
        On Error GoTo 0
    End Sub


    Public Function QuitaAcentos(ByVal Entrada As String) As String
        Dim i As Integer
        Dim DSal As String
        DSal = ""
        For i = 1 To Len(Entrada)
            'FIXIT: Replace 'Mid' function with 'Mid$' function                                        FixIT90210ae-R9757-R1B8ZE
            Select Case Mid(Entrada, i, 1)
                Case "ó"
                    DSal = DSal + "o"
                Case "Ó"
                    DSal = DSal + "O"
                Case "ò"
                    DSal = DSal + "o"
                Case "Ò"
                    DSal = DSal + "O"
                Case "á"
                    DSal = DSal + "a"
                Case "Á"
                    DSal = DSal + "A"
                Case "À"
                    DSal = DSal + "A"
                Case "à"
                    DSal = DSal + "a"
                Case "é"
                    DSal = DSal + "e"
                Case "è"
                    DSal = DSal + "e"
                Case "È"
                    DSal = DSal + "E"
                Case "É"
                    DSal = DSal + "E"
                Case "í"
                    DSal = DSal + "i"
                Case "Í"
                    DSal = DSal + "I"
                Case "ì"
                    DSal = DSal + "i"
                Case "Ì"
                    DSal = DSal + "I"
                Case "Ú"
                    DSal = DSal + "U"
                Case "ù"
                    DSal = DSal + "u"
                Case "ú"
                    DSal = DSal + "u"
                Case "Ù"
                    DSal = DSal + "U"
                Case Else
                    'FIXIT: Replace 'Mid' function with 'Mid$' function                                        FixIT90210ae-R9757-R1B8ZE
                    DSal = DSal + Mid(Entrada, i, 1)
            End Select
        Next i
        QuitaAcentos = DSal
    End Function

    Public Function RellenaCaracter(ByVal cadena As String, ByVal longitud As Integer, ByVal caracter As Char) As String
        Dim i As Integer
        For i = Len(cadena) To longitud - 1
            cadena = cadena + caracter
        Next i
        RellenaCaracter = cadena
    End Function

    Public Function Libre(ByVal longitud As Integer) As String
        Dim i As Integer
        Libre = ""
        For i = 1 To longitud
            Libre = Libre + " "
        Next i
    End Function

    Public Function ExtraeCodigoIVA(ByVal Nombre As String) As String
        Dim ds As New DataSet
        ConsultaSQL("SELECT * FROM TiposIVA WHERE Nombre='" + Nombre + "'", ds)
        If ds.Tables(0).Rows.Count > 0 Then
            ExtraeCodigoIVA = ExtraeCampo(ds.Tables(0).Rows(0).Item("NIVA"))
        Else
            ExtraeCodigoIVA = ""
        End If
    End Function

    Public Function ExtraeNombreIVA(ByVal Nombre As String) As String
        Dim ds As New DataSet
        ConsultaSQL("SELECT * FROM TiposIVA WHERE NIVA='" + Nombre + "'", ds)
        If ds.Tables(0).Rows.Count > 0 Then
            ExtraeNombreIVA = ExtraeCampo(ds.Tables(0).Rows(0).Item("Nombre"))
        Else
            ExtraeNombreIVA = "<NINGUNO>"
        End If
    End Function

    Public Function ContarCaracter(ByVal Texto As String, ByVal caracter As String, Optional ByVal Desde As Integer = 1, Optional ByVal Hasta As Integer = -1) As Integer
        'Busca un caracter o string en el string Texto y cuenta las veces que está repetido el caracter en el string
        'Texto es el string de donde se quiere buscar el caracter
        'Caracter es el string (Normalmente de un caracter) que se quiere buscar y contar
        'Devuelve en un entero la cantidad de veces que se encontro Caracter dentro de Texto
        'Por ejemplo: Si buscamos el caracter 'a' en el texto "Hasta" la funcion devolvera un 2 porque ha encontrado 2 veces el caracter 'a'.

        'PaRetsacoattros Opcionales:
        ' - Desde : Indica la posición del texto desde donde se empezara a buscar el caracter
        '           Normalmente es 1. 1 es el primer caracter de texto (Array de base 1)
        ' - Hasta : Posición del texto donde se finaliza la busqueda
        '           Normalmente es el ultimo caracter de texto [Len(Texto)]
        ' El nº de caracter Desde y Hasta de Texto se incluyen en la búsqueda
        Dim i As Integer
        If Hasta = -1 Then Hasta = Len(Texto)
        If Desde < 1 Then Desde = 1
        If Hasta < 1 Then Hasta = 1
        ContarCaracter = 0
        For i = Desde To Hasta
            If Mid(Texto, i, Len(caracter)) = caracter Then
                ContarCaracter = ContarCaracter + 1
            End If
        Next i
    End Function

    Public Function TextoNumerico(ByVal Texto As String, Optional ByVal ValorDefecto As String = "0") As String
        TextoNumerico = IIf(IsNumeric(Texto), Texto, ValorDefecto)
    End Function

    

    Public Function NoPermitirCaracteres(ByVal Texto As String, ByVal NoPermitir As String, Optional ByVal Inicio As Integer = 1, Optional ByVal Fin As Integer = -1) As String
        'En una cadena cualquiera, pasada en el argumento Texto,
        'elimina todos los caracteres que no aparezcan en la cadena Permitir.

        'El rastreo de la cadena viene dado por los argumentos Inicio y Fin
        'que por defecto la abarcan toda.

        'Generalmente sirve para filtrar la entrada del usuario en un TextBox
        'En este caso se suele asignar en los valores Inicio y Fin el Número de
        'Caracter(TEXTBOX.SelStart)que acaba de escribir el usuario.

        'Para filtrar números:       PermitirCaracteres(Texto_a_filtrar,"0123456789-,")   ; esto permite números negativos y decimales con coma
        'Para filtrar hexadecimales: PermitirCaracteres(Texto_a_filtrar,"0123456789ABCDEF")   ; esto permite numeros y cinco letras (Mayusculas)
        Dim caracter As String
        Dim Cursor As Integer
        If Fin = -1 Then Fin = Len(Texto)
        If Inicio > Len(Texto) Then Inicio = Len(Texto)
        If Fin > Len(Texto) Then Fin = Len(Texto)
        If Inicio < 1 Then Inicio = 1
        If Fin < 1 Then Fin = 1

        Cursor = Inicio
        NoPermitirCaracteres = Left(Texto, Inicio - 1) 'Añade tal cual la parte de texto inicial que no se rastreará
        While Cursor <= Fin
            caracter = Mid(Texto, Cursor, 1)
            If BuscaCaracter(NoPermitir, caracter) = -1 Then
                ' Busca dentro de la cadena de caracteres no permitidos
                ' el caracter que se está rastreando (Si no lo encuentra, es que está permitido) y lo agrega
                NoPermitirCaracteres = NoPermitirCaracteres + caracter
            End If
            Cursor = Cursor + 1
        End While
        NoPermitirCaracteres = NoPermitirCaracteres + Mid(Texto, Fin + 1) 'Añade la parte final del texto que no se rastreó
        If Err.Number <> 0 Then
            MsgBox("[NoPermitirCaracteres] Hubo un error durante la operación : " + vbCrLf + Err.Description, vbCritical)
            Err.Clear()
        End If
        On Error GoTo 0
    End Function

    Public Function PermitirCaracteres(ByVal Texto As String, ByVal Permitir As String, Optional ByVal Inicio As Integer = 1, Optional ByVal Fin As Integer = -1) As String
        PermitirCaracteres = ""
        If Inicio >= 0 And Texto.Length >= Inicio Then
            Dim textAnaliz As String = Mid(Texto, Inicio, Texto.Length)
            Dim i As Integer
            For i = 0 To textAnaliz.Length - 1
                If Permitir.IndexOf(Mid(textAnaliz, i + 1, 1)) >= 0 Then
                    PermitirCaracteres += Mid(textAnaliz, i + 1, 1)
                End If
            Next
        End If
    End Function

    Public Function BuscaCaracter(ByVal Texto As String, ByVal caracter As String) As Integer
        'Busca un caracter en una cadena.[Caracter] es un string y por lo tanto puede contener más de un caracter
        'Busca si el string Caracter está dentro del string Texto y devuelve la posición donde lo encontró
        'La función fue diseñada para buscar un sólo caracter pero se puede buscar cualquier cadena.
        'Si se busca una cadena el valor devuelto es la posición del primer caracter de la cadena a buscar.
        On Error Resume Next
        Err.Clear()

        Dim i As Integer
        BuscaCaracter = -1
        For i = 1 To Len(Texto)
            If Mid(Texto, i, Len(caracter)) = caracter Then
                BuscaCaracter = i
                Exit Function
            End If
        Next i
        If Err.Number <> 0 Then
            MsgBox("[BuscaCaracter] Hubo un error durante la operación : " + vbCrLf + Err.Description, vbCritical)
            Err.Clear()
        End If
        On Error GoTo 0

    End Function

    Public Function TextoDecimal(ByVal Texto As String, Optional ByVal Cursor As Integer = 1) As String
        'Esta función devuelve un string en formato decimal sin signo
        'Texto es la cadena a corregir (que solo puede contener los siguientes caracteres:"01234567890,.")
        'La función corrige:
        '- Dos comas en la expresión: (2,3,4) -> (2,34)
        '- Coma inicial: (,5)->(0,5)
        'Cursor es la coma que queremos conservar, especificando un Número de caracter el cual contiene la coma
        'Posibilidades del Argumento:
        '- Omitirlo, asignarlo a cero  : La coma que se conserva es la primera de izquierda a derecha
        'o a un valor que no es válido   (2,56,32) -> (2,5632)
        '- Asignarlo a una posicion de coma :   Conservamos la coma que queremos
        '                   (2,56,32) -> (256,32) (Asignando a cursor el valor 5 (la segunda coma))
        'La función convierte los puntos a comas
        Dim NCh As Integer
        NCh = Len(Texto) 'Longitud del texto antes de ser modificado
        Texto = Replace(Texto, ".", ",") 'Convertir puntos a comas
        If Cursor > Len(Texto) Then Cursor = Len(Texto)
        If Cursor < 1 Then Cursor = 1 'Evitar un valor que no es válido
        If Texto = "," Then Texto = "0," : Cursor = Len(Texto)
        If Mid(Texto, Cursor, 1) <> "," Then 'Si el argumento Cursor no corresponde con una coma, la buscamos
            Cursor = BuscaCaracter(Texto, ",")
            If Cursor = -1 Then 'Si no tenia ninguna coma, no hay nada más que hacer
                If Len(Texto) = 0 Then TextoDecimal = "" : Exit Function
                If Asc(Mid(Texto, 1, 1)) >= Asc("0") And Asc(Mid(Texto, 1, 1)) <= Asc("9") Then TextoDecimal = CStr(CDbl(Texto)) Else TextoDecimal = Texto 'Devolvemos el texto tal y como vino
                Exit Function
            End If
        End If
        Cursor = Cursor + ContarCaracter(Texto, ",", Cursor + 1)
        'Desplaza el cursor dependiendo de las comas que hay a partir de él
        '(Corrige la eliminacion de las comas que van por detrás del cursor)
        Texto = Replace(Texto, ",", "") 'Elimina TODAS las comas
        If Cursor - NCh + Len(Texto) = 0 Then 'Ahora procedemos a insertar una coma (la que se conserva)
            Texto = "0" + "," + Mid(Texto, Cursor - NCh + Len(Texto) + 1) 'Si el primer caracter es una coma, insertamos un cero delante
        Else
            Texto = CStr(CDbl(Left(Texto, Cursor - NCh + Len(Texto)))) + "," + Mid(Texto, Cursor - NCh + Len(Texto) + 1)
            'Insertamos la coma en la posicion del cursor
        End If
        TextoDecimal = Texto
    End Function


    Public Function ExtraeCampo(ByVal Campo As Object, Optional ByVal ValorPorNull As Object = "") As Object
        Dim tipo As System.TypeCode
        tipo = Campo.GetType.GetTypeCode(Campo.GetType)
        Select Case tipo
            Case TypeCode.DBNull
                ExtraeCampo = ValorPorNull
            Case TypeCode.Empty
                ExtraeCampo = ValorPorNull
            Case TypeCode.DateTime
                ExtraeCampo = Format(Campo, "dd/MM/yy")
            Case TypeCode.Double
                'ExtraeCampo = Format(Campo, "0.00")
                ExtraeCampo = Campo
            Case TypeCode.String
                ExtraeCampo = Trim(Campo)
            Case Else
                ExtraeCampo = Campo
        End Select

    End Function

   

    Public Function EjecutaSQL(ByVal Cadena As String, Optional ByVal AbrirCerrarConexion As Boolean = True, Optional ByVal Notificar As Boolean = True) As Integer
reintentar:
        Try
            Dim Escribir As SqlCommand
            'EJECUTA EL COMANDO SQL
            Escribir = New SqlCommand(Cadena, conexion)
            If AbrirCerrarConexion Then
                conexion.Open()
            End If
            EjecutaSQL = Escribir.ExecuteNonQuery
        Catch ex As Exception
            If Notificar Then
                If MsgBox("Error: " & ex.Message & vbCrLf & " Check the wireless connection. ¿Do you want to retry?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    GoTo reintentar
                End If
            End If
            EjecutaSQL = 0
        Finally
            If AbrirCerrarConexion Then
                conexion.Close()
            End If
        End Try
    End Function

    Public Function EjecutaSQL2(ByVal Cadena As String) As Boolean
        Try
            Dim Escribir As SqlCommand
            'EJECUTA EL COMANDO SQL
            Escribir = New SqlCommand(Cadena, Conexion)
            Conexion.Open()
            Escribir.ExecuteNonQuery()
            Conexion.Close()
            EjecutaSQL2 = True
        Catch ex As Exception
            MsgBox("Se ha producido un error al ejecutar el siguiente comando:" + vbCrLf _
                 + Cadena + vbCrLf + "Error: " + Err.Description, MsgBoxStyle.Critical)
            EjecutaSQL2 = False
        End Try
    End Function

    Public Function Rellena(ByVal Texto As String, ByVal NumDigitos As Byte) As String
        Dim Ceros As String
        Dim i As Byte
        If Not Len(Texto) >= NumDigitos Then
            Ceros = ""
            For i = 1 To Math.Abs(Len(Texto) - NumDigitos)
                Ceros = Ceros + "0"
            Next
            Rellena = Ceros + Texto
        Else
            Rellena = Texto
        End If

    End Function


    Public Function ConsultaSQL(ByVal Consulta As String, ByRef Datos As DataSet) As Boolean
        'PROCEDIMIENTO PARA REALIZAR UNA CONSULTA SQL
        'LOS PARAMETROS SON 
        'Consulta: Es la cadena que contiene la consulta
        'Datos: es de tipo Dataset y son los datos resultado de la consulta

        Try
            Conexion.Open()
            Conexion.Close()
        Catch ex As Exception
            MsgBox("Atención: No existe conexión con la base de datos", MsgBoxStyle.Critical)
            ConsultaSQL = False
            Exit Function
        End Try

        Try
            Dim Adaptador As SqlDataAdapter
            Datos = New DataSet
            Adaptador = New SqlDataAdapter(Consulta, Conexion)
            Adaptador.Fill(Datos)
            Adaptador.Dispose()
            Conexion.Close()
            ConsultaSQL = True
        Catch ex2 As Exception
            MsgBox("Sucedió un error ejecutando la siguiente consulta:" & vbCrLf & Consulta & vbCrLf & "Error: " & ex2.Message, MsgBoxStyle.Critical)
            ConsultaSQL = False
        End Try

    End Function

    Public Function ConsultaSQL(ByVal Consulta As String, ByRef Datos As DataTable, Optional ByVal notificar As Boolean = True) As Integer
        'PROCEDIMIENTO PARA REALIZAR UNA CONSULTA SQL DEL TIPO 'SELECT'
        'LOS PARAMETROS SON 
        'Consulta: Es la cadena que contiene la consulta
        'Datos: es de tipo Dataset y son los datos resultado de la consulta
reintentar:
        Try
            Datos.Rows.Clear()
            Dim Adaptador As SqlDataAdapter
            Adaptador = New SqlDataAdapter(Consulta, conexion)
            Adaptador.Fill(Datos)
            Adaptador.Dispose()
            ConsultaSQL = 1
        Catch ex As Exception
            If notificar Then
                If MsgBox("Error: " & ex.Message & vbCrLf & " Check the wireless connection. ¿Do you want to retry?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    GoTo reintentar
                End If
                ConsultaSQL = 0
            End If
            ConsultaSQL = 0
        Finally
            conexion.Close()
        End Try

    End Function


    Public Sub ConsultaSQL(ByVal Consulta As String, ByRef Datos As DataTable, ByVal Conex As SqlConnection, Optional ByVal notificar As Boolean = True)
        'PROCEDIMIENTO PARA REALIZAR UNA CONSULTA SQL DEL TIPO 'SELECT'
        'LOS PARAMETROS SON 
        'Consulta: Es la cadena que contiene la consulta
        'Datos: es de tipo Dataset y son los datos resultado de la consulta
        Try
            Datos.Rows.Clear()
            Dim Adaptador As SqlDataAdapter
            Adaptador = New SqlDataAdapter(Consulta, Conex)
            Adaptador.Fill(Datos)
            Adaptador.Dispose()
        Catch ex As Exception
            If notificar Then
                MsgBox("Sucedió un error ejecutando la siguiente consulta:" & vbCrLf _
                                & Consulta & vbCrLf & "Error: " & ex.Message, MsgBoxStyle.Critical)
            End If
        Finally
            conexion.Close()
        End Try
    End Sub

    'Public Sub ConsultaSQL2(ByVal Consulta As String, ByRef Datos As DatosImp, Optional ByVal Tabla As Object = 0)
    '    'PROCEDIMIENTO PARA REALIZAR UNA CONSULTA SQL DEL TIPO 'SELECT'
    '    'LOS PARAMETROS SON 
    '    'Consulta: Es la cadena que contiene la consulta
    '    'Datos: es de tipo Dataset y son los datos resultado de la consulta
    '    Dim Adaptador As SqlDataAdapter
    '    On Error Resume Next
    '    Err.Clear()
    '    Datos = New DatosImp
    '    Adaptador = New SqlDataAdapter(Consulta, Conexion)
    '    Adaptador.Fill(Datos, Tabla)
    '    Adaptador.Dispose()
    '    Conexion.Close()
    '    If Err.Number <> 0 Then
    '        MsgBox("Sucedió un error ejecutando la siguiente consulta:" & vbCrLf _
    '            & Consulta & vbCrLf & "Error: " & Err.Description, MsgBoxStyle.Critical)
    '    End If
    '    On Error GoTo 0
    'End Sub

    Public Sub ConsultaSQL3(ByVal Consulta As String, ByRef Datos As DataSet, Optional ByVal Tabla As Object = 0)
        'PROCEDIMIENTO PARA REALIZAR UNA CONSULTA SQL DEL TIPO 'SELECT'
        'LOS PARAMETROS SON 
        'Consulta: Es la cadena que contiene la consulta
        'Datos: es de tipo Dataset y son los datos resultado de la consulta
        Dim Adaptador As SqlDataAdapter
        On Error Resume Next
        Err.Clear()
        Adaptador = New SqlDataAdapter(Consulta, Conexion)
        Adaptador.Fill(Datos, Tabla)
        Adaptador.Dispose()
        Conexion.Close()
        If Err.Number <> 0 Then
            MsgBox("Sucedió un error ejecutando la siguiente consulta:" & vbCrLf _
                & Consulta & vbCrLf & "Error: " & Err.Description, MsgBoxStyle.Critical)
        End If
        On Error GoTo 0
    End Sub

    Public Function CompruebaFecha(ByVal Fecha As String) As Boolean
        'PEQUEÑA FUNCIÓN PARA COMPROBAR SI UNA CADENA CONTIENE UNA FECHA VÁLIDA
        Dim TipoFecha As Date
        On Error Resume Next
        Err.Clear()
        TipoFecha = CDate(Fecha)
        If Err.Number <> 0 Then
            CompruebaFecha = False
        Else
            CompruebaFecha = True
        End If
        On Error GoTo 0
    End Function

    Public Function CompruebaFloat(ByVal Numero As String) As Boolean
        'PEQUEÑA FUNCIÓN PARA COMPROBAR SI UNA CADENA CONTIENE UN NÚMERO REAL VÁLIDO
        Dim Resultado As Double
        On Error Resume Next
        Err.Clear()
        Resultado = CDbl(Numero)
        If Err.Number <> 0 Then
            CompruebaFloat = False
        Else
            CompruebaFloat = True
        End If
        On Error GoTo 0
    End Function

    Public Function NumeroMes(ByVal Mes As String) As Integer
        Select Case UCase(Mes)
            Case "ENERO"
                NumeroMes = "1"
            Case "FEBRERO"
                NumeroMes = "2"
            Case "MARZO"
                NumeroMes = "3"
            Case "ABRIL"
                NumeroMes = "4"
            Case "MAYO"
                NumeroMes = "5"
            Case "JUNIO"
                NumeroMes = "6"
            Case "JULIO"
                NumeroMes = "7"
            Case "AGOSTO"
                NumeroMes = "8"
            Case "SEPTIEMBRE"
                NumeroMes = "9"
            Case "OCTUBRE"
                NumeroMes = "10"
            Case "NOVIEMBRE"
                NumeroMes = "11"
            Case "DICIEMBRE"
                NumeroMes = "12"
            Case Else
                NumeroMes = "0"
        End Select
    End Function

    Public Function TextoMes(ByVal Mes As Integer) As String
        Select Case Mes
            Case 1
                TextoMes = "Enero"
            Case 2
                TextoMes = "Febrero"
            Case 3
                TextoMes = "Marzo"
            Case 4
                TextoMes = "Abril"
            Case 5
                TextoMes = "Mayo"
            Case 6
                TextoMes = "Junio"
            Case 7
                TextoMes = "Julio"
            Case 8
                TextoMes = "Agosto"
            Case 9
                TextoMes = "Septiembre"
            Case 10
                TextoMes = "Octubre"
            Case 11
                TextoMes = "Noviembre"
            Case 12
                TextoMes = "Diciembre"
            Case Else
                TextoMes = ""
        End Select
    End Function


    Public Function Desencripta(ByVal pas As String) As String
        Dim a As Long
        Dim resultado As String
        resultado = ""
        a = 1
        While Mid(pas, a, 1) <> ""
            resultado = resultado + Chr(Val(Mid(pas, a, 3)))
            a = a + 3
        End While
        Desencripta = resultado
    End Function

    Public Function Encripta(ByVal pas As String) As String
        Dim a As Long
        Dim resultado As String
        resultado = ""
        a = 1
        While Mid(pas, a, 1) <> ""
            resultado = resultado + Rellena(Str(Asc(Mid(pas, a, 1))), 3)
            a = a + 1
        End While
        Encripta = resultado
    End Function

    Public Sub ComboSQL(ByVal Cadena As String, ByRef Combo As ComboBox)
        Dim Datos As New DataSet
        Dim Row As DataRow
        If Not ConsultaSQL(Cadena, Datos) Then Exit Sub
        Combo.Items.Clear()
        If Datos.Tables(0).Rows.Count > 0 Then
            For Each Row In Datos.Tables(0).Rows
                Combo.Items.Add(ExtraeCampo(Row.Item(0)))
            Next
            Combo.SelectedIndex = 0
        End If
    End Sub

    Public Sub ComboSQL2(ByVal Cadena As String, ByRef Combo As ComboBox)
        Dim Datos As New DataSet
        Dim Row As DataRow
        If Not ConsultaSQL(Cadena, Datos) Then Exit Sub
        Combo.Items.Clear()
        If Datos.Tables(0).Rows.Count > 0 Then
            Combo.Items.Add("< Todos >")
            For Each Row In Datos.Tables(0).Rows
                Combo.Items.Add(ExtraeCampo(Row.Item(0)))
            Next
            Combo.SelectedIndex = 0
        End If
    End Sub

    Public Sub ComboSQL3(ByVal Cadena As String, ByRef Combo As ComboBox, Optional ByVal Elemento1 As String = "")
        Dim Datos As New DataSet
        Dim Row As DataRow
        If Not ConsultaSQL(Cadena, Datos) Then Exit Sub
        Combo.Items.Clear()
        If Datos.Tables(0).Rows.Count > 0 Then
            If Elemento1 <> "" Then
                Combo.Items.Add(Elemento1)
            End If
            For Each Row In Datos.Tables(0).Rows
                Combo.Items.Add(ExtraeCampo(Row.Item(0)))
            Next
            Combo.SelectedIndex = 0
        End If
    End Sub

    'TRADUCCION DE NUMERO A TEXTO
    Private Function unidad(ByVal entero As Integer) As String
        Select Case entero
            Case 0 : unidad = ""
            Case 1 : unidad = "un"
            Case 2 : unidad = "dos"
            Case 3 : unidad = "tres"
            Case 4 : unidad = "cuatro"
            Case 5 : unidad = "cinco"
            Case 6 : unidad = "seis"
            Case 7 : unidad = "siete"
            Case 8 : unidad = "ocho"
            Case 9 : unidad = "nueve"
        End Select
    End Function

    Private Function d10_19(ByVal entero As Integer) As String
        Select Case entero
            Case 10 : d10_19 = "diez"
            Case 11 : d10_19 = "once"
            Case 12 : d10_19 = "doce"
            Case 13 : d10_19 = "trece"
            Case 14 : d10_19 = "catorce"
            Case 15 : d10_19 = "quince"
            Case 16 : d10_19 = "dieciseis"
            Case 17 : d10_19 = "diecisiete"
            Case 18 : d10_19 = "dieciocho"
            Case 19 : d10_19 = "diecinueve"
        End Select
    End Function

    Private Function d20_29(ByVal entero As Integer) As String

        If entero = 20 Then
            d20_29 = "veinte"
        Else
            d20_29 = "veinti" + unidad(entero - 20)
        End If
    End Function

    Private Function decena(ByVal entero As Integer) As String
        Dim ldecena As String

        If entero < 10 Then
            decena = unidad(entero)
        Else
            Select Case entero
                Case 10 To 19 : decena = d10_19(entero)
                Case 20 To 29 : decena = d20_29(entero)
                Case Else
                    Select Case entero
                        Case 30 To 39 : ldecena = "treinta"
                        Case 40 To 49 : ldecena = "cuarenta"
                        Case 50 To 59 : ldecena = "cincuenta"
                        Case 60 To 69 : ldecena = "sesenta"
                        Case 70 To 79 : ldecena = "setenta"
                        Case 80 To 89 : ldecena = "ochenta"
                        Case 90 To 99 : ldecena = "noventa"
                    End Select

                    If (entero Mod 10) = 0 Then
                        decena = ldecena
                    Else
                        decena = ldecena + " y " + unidad(entero Mod 10)
                    End If
            End Select
        End If
    End Function

    Private Function centena(ByVal entero As Integer) As String

        If entero < 100 Then
            centena = decena(entero)
        Else
            Select Case entero
                Case 100 : centena = "cien"
                Case 101 To 199 : centena = "ciento " + decena(entero Mod 100)
                Case 700 To 799 : centena = "setecientos " + decena(entero Mod 100)
                Case 500 To 599 : centena = "quinientos " + decena(entero Mod 100)
                Case 900 To 999 : centena = "novecientos " + decena(entero Mod 100)
                Case Else
                    centena = unidad((entero - (entero Mod 100)) / 100) + "cientos " + decena(entero Mod 100)
            End Select
        End If
    End Function

    Private Function Miles(ByVal entero As Long) As String

        If entero < 1000 Then
            Miles = centena(CInt(entero))
        ElseIf entero >= 2000 Then
            Miles = centena((entero - (entero Mod 1000)) / 1000) + " mil " + centena(entero Mod 1000)
        Else
            Miles = "mil " + centena(entero Mod 1000)
        End If

    End Function

    Private Function Millones(ByVal entero As Long) As String
        Dim miles_de_millon As Long

        If entero < 1000000 Then
            Millones = Miles(CLng(entero))
        Else
            miles_de_millon = (entero - ((entero Mod 1000000))) / 1000000

            Millones = Miles(miles_de_millon)
            If miles_de_millon = 1 Then
                Millones = Millones + " millon " + Miles(entero Mod 1000000)
            Else
                Millones = Millones + " millones " + Miles(entero Mod 1000000)
            End If
        End If

    End Function

    Function Moneda_A_Letras(ByVal numero As Double) As String
        Dim entero, decimales As String

        If numero > 999999999 Then
            Moneda_A_Letras = "Llame al Proveedor del programa, ese numero no se puede imprimir"
        Else
            'decimales = Millones(CLng((numero - Fix(numero)) * 100))
            decimales = (numero - Fix(numero)) * 100
            entero = Millones(Fix(numero))
            If IsNumeric(decimales) Then
                decimales = Millones(Fix(CDbl(Format(CDbl(decimales), "0.00"))))
            End If

            Select Case Fix(numero)
                Case 0 : Moneda_A_Letras = decimales + " centimos"
                Case 1
                    If decimales = "" Then
                        Moneda_A_Letras = entero + " euro"
                    Else
                        Moneda_A_Letras = entero + " euro con " + decimales + " céntimos"
                    End If
                Case Else
                    If decimales = "" Then
                        Moneda_A_Letras = entero + " euros"
                    Else
                        Moneda_A_Letras = entero + " euros con " + decimales + " céntimos"
                    End If
            End Select
        End If
    End Function

    Function Num_A_Letras(ByVal numero As Double) As String
        Dim entero, decimales As String

        If numero > 999999999 Then
            Num_A_Letras = "Llame al Proveedor del programa, ese numero no se puede imprimir"
        Else
            decimales = decena((numero - Fix(numero)) * 100)
            entero = Millones(Fix(numero))

            If decimales = "" Then
                Num_A_Letras = entero
            Else
                Num_A_Letras = entero + " punto " + decimales
            End If
        End If
    End Function

    Public Function QuitarAcentos(ByVal Texto As String) As String
        'Quita los acentos y las dieresis del argumento Texto y devuelve el string resultante
        Dim i As Integer
        Dim Ch As String
        QuitarAcentos = ""
        For i = 1 To Len(Texto)
            Ch = Mid(Texto, i, 1)
            If Ch = "á" Or Ch = "à" Or Ch = "ä" Or Ch = "â" Then Ch = "a"
            If Ch = "é" Or Ch = "è" Or Ch = "ë" Or Ch = "ê" Then Ch = "e"
            If Ch = "í" Or Ch = "ì" Or Ch = "ï" Or Ch = "î" Then Ch = "i"
            If Ch = "ó" Or Ch = "ò" Or Ch = "ö" Or Ch = "ô" Then Ch = "o"
            If Ch = "ú" Or Ch = "ù" Or Ch = "ü" Or Ch = "û" Then Ch = "u"

            If Ch = "Á" Or Ch = "À" Or Ch = "Ä" Or Ch = "Â" Then Ch = "A"
            If Ch = "É" Or Ch = "È" Or Ch = "Ë" Or Ch = "Ê" Then Ch = "E"
            If Ch = "Í" Or Ch = "Ì" Or Ch = "Ï" Or Ch = "Î" Then Ch = "I"
            If Ch = "Ó" Or Ch = "Ò" Or Ch = "Ö" Or Ch = "Ô" Then Ch = "O"
            If Ch = "Ú" Or Ch = "Ù" Or Ch = "Ü" Or Ch = "Û" Then Ch = "U"

            If Ch = "º" Or Ch = "ª" Then Ch = Chr(248)
            If Ch = "Ñ" Then Ch = "N"
            If Ch = "ñ" Then Ch = "n"
            If Ch = "Ç" Then Ch = "C"
            If Ch = "ç" Then Ch = "c"
            If Ch = "'" Then Ch = Chr(39)
            If Ch = "´" Then Ch = Chr(39)
            If Ch = "`" Then Ch = Chr(39)

            QuitarAcentos = QuitarAcentos + Ch
        Next i

    End Function

    '******************************************************************************
    '
    ' Propósito: Calcular el Dígito de control para los:
    '            NIF - Número de Identificación Fiscal.
    '            NIE - Número Identificador de Extranjeros.
    '            CIF - Código de Identificación Fiscal.
    '
    ' Entradas: Una cadena compuesta por el DNI,NIF,CIF,NIE.
    '
    ' Devuelve: Una cadena con el Código de Control, número ó letra
    '           según corresponda. Si se produce un error retorna ""
    '
    ' Requiere las funciones:
    '           DigitoNIF() - Calcula la letra del NIF
    '           DigitoCIF() - Calcula el número/letra del CIF
    '
    '******************************************************************************
    Public Function DigitoCIFNIF(ByVal strCifNif As String) As String
        Dim strTemp As String, strLetra As String
        Dim lngTam As Long, i As Long

        ' iniciar y así, si salimos por algún error, que podamos detectarlo
        ' en la función/procedimiento de llamada
        DigitoCIFNIF = ""

        lngTam = Len(strCifNif)

        strCifNif = UCase(strCifNif) ' convertir en mayúsculas

        ' quitar los espacios en blanco y los carácteres - y /
        For i = 1 To lngTam
            strLetra = Mid$(strCifNif, i, 1)
            If strLetra = "-" Or strLetra = "/" Or strLetra = " " Then
                ' no hace nada - se lo salta
            Else
                strTemp = strTemp & strLetra ' monta la cadena letra a letra
            End If
        Next i

        strCifNif = strTemp

        ' si el primer carácter es un número tratarlo como un DNI -> NIF
        If IsNumeric(Mid$(strCifNif, 1, 1)) Then
            DigitoCIFNIF = DigitoNIF(CLng(Val(strCifNif)))
        Else ' es un CIF - NIE
            ' si es un NIE procesarlo como un DNI -> NIF
            If Mid$(strCifNif, 1, 1) = "X" Then ' es un NIE
                DigitoCIFNIF = DigitoNIF(CLng(Val(Mid$(strCifNif, 2, lngTam - 1))))
            Else ' es un CIF
                ' tiene los 8 carácteres necesarios para la comprobación del CIF ?
                If lngTam < 8 Then
                    MsgBox("CIF incompleto, mínimo 8 carácteres.", vbOKOnly + vbCritical, "ATENCIÓN")
                    Exit Function
                Else
                    ' comienza por alguna de las letras admitidas para los CIF ?
                    If InStr(1, "ABCDEFGHKLMNPQS", Left(strCifNif, 1)) > 0 Then
                        DigitoCIFNIF = DigitoCIF(strCifNif)
                    Else
                        MsgBox("La Primera letra no corresponde a un CIF.", vbOKOnly + vbCritical, "ATENCIÓN")
                        Exit Function
                    End If
                End If
            End If
        End If

    End Function

    '******************************************************************************
    '
    ' Propósito: Calcular la letra del NIF
    '
    ' Entradas: Un entero largo con el número de DNI
    '
    ' Devuelve: Una cadena con la letra de control del NIF.
    '
    '******************************************************************************
    Private Function DigitoNIF(ByVal lngDNI As Long) As String

        DigitoNIF = Mid$("TRWAGMYFPDXBNJZSQVHLCKE", (lngDNI Mod 23) + 1, 1)

    End Function

    '******************************************************************************
    '
    ' Propósito: Calcular el Dígito de control para los CIF
    '
    ' Entradas: Una cadena compuesta por el CIF.
    '
    ' Devuelve: Una cadena con el Código de Control, número y letra
    ' según la relación siguiente:
    '
    ' A ó 1    B ó 2    C ó 3    D ó 4    E ó 5
    ' F ó 6    G ó 7    H ó 8    I ó 9    J ó 0
    '
    ' El último dígito, que es el de control, puede contener
    ' la letra ó el número indistintamente, por ello la función
    ' retorna el par letra-número para poder comprobarlo.
    '
    '******************************************************************************
    Private Function DigitoCIF(ByVal strCif As String) As String
        Const conValores As String = "0246813579"
        Dim i As Long, lngTemp As Long

        DigitoCIF = ""
        lngTemp = 0

        For i = 2 To 6 Step 2
            lngTemp = lngTemp + Val(Mid$(conValores, Val(Mid$(strCif, i, 1)) + 1, 1))
            lngTemp = lngTemp + Val(Mid$(strCif, i + 1, 1))
        Next i

        lngTemp = lngTemp + Val(Mid$(conValores, Val(Mid$(strCif, 8, 1)) + 1, 1))

        lngTemp = 10 - (lngTemp Mod 10)

        If lngTemp = 10 Then
            DigitoCIF = "J ó 0"
        Else
            DigitoCIF = Chr(lngTemp + 64) & " ó " & Str(lngTemp)
        End If

    End Function

    'FUNCION PARA CALCULAR QUE UNA CUENTA BANCARIA SEA CORRECTA
    Public Function CalcularCuentaCorrecta(ByVal Entidad As String, ByVal Oficina As String, ByVal DC As String, ByVal Cuenta As String) As Boolean
        If Len(Trim(Entidad)) <= 0 And Len(Trim(Oficina)) <= 0 And Len(Trim(DC)) <= 0 And Len(Trim(Cuenta)) <= 0 Then
            CalcularCuentaCorrecta = True
            Exit Function
        ElseIf Len(Entidad) <> 4 Or Len(Oficina) <> 4 Or Len(DC) <> 2 Or Len(Cuenta) <> 10 Then
            CalcularCuentaCorrecta = False
            Exit Function
        End If
        Dim Suma1, Suma2, DC1, DC2 As Integer
        Suma1 = Mid(Entidad, 1, 1) * 4 + Mid(Entidad, 2, 1) * 8 + Mid(Entidad, 3, 1) * 5 + Mid(Entidad, 4, 1) * 10 + Mid(Oficina, 1, 1) * 9 + Mid(Oficina, 2, 1) * 7 + Mid(Oficina, 3, 1) * 3 + Mid(Oficina, 4, 1) * 6
        Suma2 = Mid(Cuenta, 1, 1) * 1 + Mid(Cuenta, 2, 1) * 2 + Mid(Cuenta, 3, 1) * 4 + Mid(Cuenta, 4, 1) * 8 + Mid(Cuenta, 5, 1) * 5 + Mid(Cuenta, 6, 1) * 10 + Mid(Cuenta, 7, 1) * 9 + Mid(Cuenta, 8, 1) * 7 + Mid(Cuenta, 9, 1) * 3 + Mid(Cuenta, 10, 1) * 6
        DC1 = 11 - (Suma1 Mod 11)
        DC2 = 11 - (Suma2 Mod 11)
        If DC1 = 10 Then DC1 = 1
        If DC1 = 11 Then DC1 = 0
        If DC2 = 10 Then DC2 = 1
        If DC2 = 11 Then DC2 = 0
        If DC = CStr(DC1) + CStr(DC2) Then
            CalcularCuentaCorrecta = True
        Else
            CalcularCuentaCorrecta = False
        End If
    End Function

    'FUNCION PARA COMPROBAR NIF
    Public Function ComprobarNIF(ByVal NIF As String) As Boolean
        Dim letra As String

        letra = Mid$("TRWAGMYFPDXBNJZSQVHLCKE", (Mid(NIF, 1, 8) Mod 23) + 1, 1)

        If Mid(NIF, 9, 1).ToLower = letra.ToLower Then
            ComprobarNIF = True
        Else
            ComprobarNIF = False
        End If

    End Function

    'FUNCION PARA COMPROBAR CIF
    Public Function ComprobarCIF(ByVal strCif As String) As Boolean
        Const conValores As String = "0246813579"
        Dim i As Long, lngTemp, Digito As Long
        Dim Letra As String

        Letra = ""
        lngTemp = 0

        If InStr(1, "ABCDEFGHKLMNPQS", Left(strCif, 1).ToUpper) = 0 Then
            ComprobarCIF = False
            Exit Function
        End If

        For i = 2 To 6 Step 2
            lngTemp = lngTemp + Val(Mid$(conValores, Val(Mid$(strCif, i, 1)) + 1, 1))
            lngTemp = lngTemp + Val(Mid$(strCif, i + 1, 1))
        Next i

        lngTemp = lngTemp + Val(Mid$(conValores, Val(Mid$(strCif, 8, 1)) + 1, 1))

        lngTemp = 10 - (lngTemp Mod 10)

        If lngTemp = 10 Then
            Letra = "J"
            Digito = 0
        Else
            Letra = Chr(lngTemp + 64)
            Digito = Str(lngTemp)
        End If

        If IsNumeric(Mid(strCif, 9, 1)) Then
            If Digito = Mid(strCif, 9, 1) Then
                ComprobarCIF = True
            Else
                ComprobarCIF = False
            End If
        Else
            If Letra.ToLower = Mid(strCif, 9, 1).ToLower Then
                ComprobarCIF = True
            Else
                ComprobarCIF = False
            End If
        End If

    End Function

    Public Function ComprobarCIFNIF(ByVal strCifNif As String) As Boolean
        If Len(Trim(strCifNif)) <= 0 Then
            ComprobarCIFNIF = True
            Exit Function
        End If
        If IsNumeric(Mid(strCifNif, 1, 1)) Then
            ComprobarCIFNIF = ComprobarNIF(strCifNif)
        Else
            ComprobarCIFNIF = ComprobarCIF(strCifNif)
        End If
    End Function


    '******************************************************************************
    '
    ' Propósito: Obtener una cadena que defina
    '            el tipo de documento NIF,CIF,NIE.
    '
    ' Entradas: Una cadena compuesta por la primera letra
    '           del NIF,CIF,NIE.
    '
    ' Devuelve: Una cadena con la descripción de tipo. Si no puede localizar
    '           el tipo de documento devuelve la cadena "ERROR"
    '
    '******************************************************************************

    Public Function DescripNIFCIF(ByVal strLetra As String) As String
        Dim strTipo(18) As String
        Dim intIndice As Integer

        strTipo(0) = "NIF - Número de Identificación Fiscal."
        strTipo(1) = "A - Sociedad Anónima."
        strTipo(2) = "B - Sociedad de responsabilidad limitada."
        strTipo(3) = "C - Sociedad colectiva."
        strTipo(4) = "D - Sociedad comanditaria."
        strTipo(5) = "E - Comunidad de bienes."
        strTipo(6) = "F - Sociedad cooperativa."
        strTipo(7) = "G - Asociación."
        strTipo(8) = "H - Comunidad de propietarios."
        strTipo(9) = "K - Formato antiguo."
        strTipo(10) = "L - Formato antiguo."
        strTipo(11) = "M - Formato antiguo."
        strTipo(12) = "N - Formato antiguo."
        strTipo(13) = "P - Corporación local."
        strTipo(14) = "Q - Organismo autónomo."
        strTipo(15) = "S - Organo de la administración."
        strTipo(16) = "X - NIE - Número Identificador de Extranjeros."
        strTipo(17) = "ERROR"


        If IsNumeric(strLetra) Then
            intIndice = 0
        Else
            intIndice = InStr(1, "ABCDEFGHKLMNPQSX", strLetra, 1)
            If intIndice = 0 Then
                intIndice = 17
            End If
        End If

        DescripNIFCIF = strTipo(intIndice)

    End Function


    Public Sub GuardaDatos(ByVal TablaDatos As String, ByVal Campos As String, ByVal Datos As String)
        Dim Escribir As SqlCommand
        Dim Cadena As String
        'GUARDA LOS DATOS EN LA B.D.
        On Error Resume Next
        Cadena = "INSERT INTO " & TablaDatos & " (" & Campos & ") VALUES(" & Datos & ")"
        Escribir = New SqlCommand(Cadena, Conexion)
        Conexion.Open()
        Escribir.ExecuteNonQuery()
        Conexion.Close()
        If Err.Number <> 0 Then
            MsgBox("Se ha producido un error al ejecutar el siguiente comando:" + vbCrLf _
                + Cadena + vbCrLf + "Error: " + Err.Description, MsgBoxStyle.Critical)
        End If
        On Error GoTo 0
    End Sub

    Public Function GenerarSubcuentas2(ByVal TablaI As ListView, ByVal Ruta As String) As String
        Dim check As Boolean
        Dim i As Integer
        Dim ds As DataSet
        Dim NFactura, Cuenta, NProveedor, Nombre As String
        Dim NIF, Direccion, Poblacion, Provincia, CPostal As String
        Dim row As DataRow

        Dim sw As New StreamWriter(Ruta, False, System.Text.Encoding.ASCII, 10000)

        For i = 0 To TablaI.Items.Count - 1
            check = TablaI.Items(i).Checked
            If check Then
                NFactura = TablaI.Items(i).Text

                If Not ConsultaSQL("SELECT Proveedores.* FROM FacturasEntrada INNER JOIN Proveedores ON FacturasEntrada.NProveedor = Proveedores.NProveedor WHERE FacturasEntrada.NFactura='" + NFactura + "'", ds) Then Exit Function
                NProveedor = ExtraeCampo(ds.Tables(0).Rows(0).Item("NProveedor"), 0)
                Cuenta = ExtraeCampo(ds.Tables(0).Rows(0).Item("CodContable"), "")
                Nombre = QuitaAcentos(ExtraeCampo(ds.Tables(0).Rows(0).Item("Nombre"), ""))
                NIF = ExtraeCampo(ds.Tables(0).Rows(0).Item("CIF"), "")
                Poblacion = QuitaAcentos(ExtraeCampo(ds.Tables(0).Rows(0).Item("Poblacion"), ""))
                Direccion = QuitaAcentos(ExtraeCampo(ds.Tables(0).Rows(0).Item("Direccion"), ""))
                Provincia = QuitaAcentos(ExtraeCampo(ds.Tables(0).Rows(0).Item("provincia"), ""))
                CPostal = ExtraeCampo(ds.Tables(0).Rows(0).Item("CPostal"), "")

                'INSERTAR SUBCUENTA
                sw.WriteLine(RellenaCar2(Cuenta, 12, " ") + RellenaCar2(Left(Nombre, 40), 40, " ") + RellenaCar2(NIF, 15, " ") + RellenaCar2(Left(Direccion, 35), 35, " ") + RellenaCar2(Left(Poblacion, 25), 25, " ") + RellenaCar2(Left(Provincia, 20), 20, " ") + RellenaCar2(Left(CPostal, 5), 5, " ") + "F" + "     " + "F" + "F" + " ")
            End If
        Next i

        sw.Flush()
        sw.Close()

    End Function

   

    Public Function CalculoIBAN(ByVal Entidad As String, ByVal Oficina As String, ByVal DC As String, ByVal Cuenta As String) As String
        Dim Calculostr As String
        Dim Calculo As Decimal
        'CÁLCULO DE IBAN PARA CUENTAS ESPAÑOLAS
        Calculostr = Entidad + Oficina + DC + Cuenta + "14" + "28" + "00"
        Calculo = Calculostr
        CalculoIBAN = "ES" + Format(98 - (Calculo Mod 97), "00")
    End Function






    Public Function RellenaCar(ByVal cadena As String, ByVal Numero As Integer, Optional ByVal Ch As String = "0") As String
        Dim num As String
        Dim Cantidad As Integer
        Dim i As Integer
        On Error Resume Next
        Err.Clear()
        cadena = Left(cadena, Numero)
        num = cadena
        Cantidad = Len(num)
        While Len(num) < Numero
            num = Ch + num
        End While
        RellenaCar = num
        If Err.Number <> 0 Then
            MsgBox("[RellenaCar] Hubo un error durante la operación : " + vbCrLf + Err.Description, vbCritical)
            Err.Clear()
        End If
        On Error GoTo 0
    End Function

    Public Function RellenaCar2(ByVal cadena As String, ByVal Numero As Integer, Optional ByVal Ch As String = "0") As String
        Dim num As String
        Dim Cantidad As Integer
        Dim i As Integer
        On Error Resume Next
        Err.Clear()
        cadena = Left(cadena, Numero)
        num = cadena
        Cantidad = Len(num)
        While Len(num) < Numero
            num = num + Ch
        End While
        RellenaCar2 = num
        If Err.Number <> 0 Then
            MsgBox("[RellenaCar] Hubo un error durante la operación : " + vbCrLf + Err.Description, vbCritical)
            Err.Clear()
        End If
        On Error GoTo 0
    End Function

    Public Function SqlStr(ByVal Texto As String) As String
        SqlStr = Replace(Texto, "'", "''")
        Return SqlStr
    End Function

    Public Function SqlFecha(ByVal Fecha As DateTime, Optional ByVal HorasMinutosSegundos As Boolean = True) As String
        SqlFecha = Fecha.ToString("yyyyMMdd ") & IIf(HorasMinutosSegundos, Fecha.ToString("HH:mm:ss"), "00:00:00")
        Return SqlFecha
    End Function

    Public Function SqlNumero(ByVal Numero As String) As String
        SqlNumero = Replace(Numero, ",", ".")
        Return SqlNumero
    End Function
End Module






