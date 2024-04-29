Imports System.Net.Sockets
Imports System.Data.SqlClient
Imports System.Data


Module mMain

 


    Public cerrandoApp As Boolean = False
    Public client As TcpClient
    Public stream As NetworkStream
    Public NPreparacion As String
    Public NBulto As String
    Public Pantalla As String
 
    Public ImpresoraDefecto As String = ""
    Public IdPistola As String = ""

  

    Public AsociadoAnt As Boolean = False

    Public BaseDatos As String = "MFGPRO_SQL"
  

    Public passBD As String = "Sql2016"
    Public Servidor As String = "A7402M100"
    Public userBD As String = "sqluser"
   

    '//Declarando el WebService

    Public Argumentos() As String
    Public FormularioInicio As Integer
    Public ArrancadoMenu As Boolean = False

    'Public Servidor As String = "(local)"
    'Public userBD As String = "sa"

    Public conexion As SqlConnection
    Public cadenaConexion As String = ""
    Public NOperario As String = ""
    Public NombreOperario As String = ""


     
    Structure Bulto
        Dim codigo As String
        Dim ubicacionActual As String
        Dim ubicacionRecomendada As String
    End Structure

   


    Public Function PingIP(ByVal IP As String)
        'Dim result As Boolean = False
        'Dim ping As New OpenNETCF.Net.NetworkInformation.Ping()

        'Dim pingReply As OpenNETCF.Net.NetworkInformation.PingReply = ping.Send(IP)

        'If pingReply.Status = OpenNETCF.Net.NetworkInformation.IPStatus.Success Then
        '    result = True
        'Else
        '    MsgBox("No Ping " & IP)
        'End If
    End Function

    Public Function CargaConexionSQL() As Boolean
        Try


            cadenaConexion = "workstation id=" & Servidor _
          & ";packet size=4096;" _
          & "data source=" & Servidor & ";" _
          & "user id=" & userBD _
          & ";pwd=" & passBD _
          & ";Connect Timeout=30" _
          & ";initial catalog=" & BaseDatos & ";"



            conexion = New SqlConnection
            conexion.ConnectionString = cadenaConexion
            conexion.Open()
            CargaConexionSQL = True
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox("No connection to the SQL. Check:" & vbCr & "  -User and password to connect database." & vbCr & "  -Server and Database name is correct." & vbCr & "  -Network connection is ok.")
            CargaConexionSQL = False
            Exit Function
        Finally
            conexion.Close()
        End Try

    End Function
    Public Function CargarDatosConfiguracion() As Boolean
        Dim errorCargando As Boolean = False
        If IO.File.Exists(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) & "\" & "Config.ini") Then
            Try
                Dim ArchivoIni As New System.IO.StreamReader(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) & "\" & "Config.ini")
                Dim sLine As String
                Do
                    sLine = ArchivoIni.ReadLine()
                    If Not sLine Is Nothing Then
                        Select Case sLine.Split("=")(0)
                            Case "BASEDATOS" : BaseDatos = sLine.Split("=")(1)
                            Case "SERVIDOR" : Servidor = sLine.Split("=")(1)
                            Case "USERBD" : userBD = sLine.Split("=")(1)
                            Case "PASSBD" : passBD = sLine.Split("=")(1)
                        End Select
                    End If
                Loop Until sLine Is Nothing
                ArchivoIni.Close()

                CargarDatosConfiguracion = True

            Catch ex As Exception
                'errorCargando = True
                'CargarDatosConfiguracion = False
            End Try

        Else
            'errorCargando = True
            'CargarDatosConfiguracion = False
        End If

    End Function
   
  
   
  


  

   
End Module
