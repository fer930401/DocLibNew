Imports System.Data
Imports System.Data.SqlClient


Namespace Doclib.libreria

    Public Class Transacciones

        Private ReadOnly _conexion As New Datos
        Private ReadOnly _cnn As SqlConnection = _conexion.MiConexion
        Private ReadOnly _timeout As Integer = _Conexion.time_out

        Public Function ConsultaEntidad() As DataTable
            Dim dtRes As New DataTable
            Dim sqlAdapter = New SqlDataAdapter("consWeb", _cnn)
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            sqlAdapter.SelectCommand.CommandTimeout = _timeout
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@opc", "entFin")
            Try
                _cnn.Open()
                sqlAdapter.Fill(dtRes)
                sqlAdapter.Dispose()
                _cnn.Close()
            Catch ex As Exception
                Dim msg As String
                msg = "consWeb:entFin"
                'iErrorG = 3
                'agrega_err(iErrorG, msg, errores)
            Finally
                If _cnn.State = ConnectionState.Open Then
                    _cnn.Close()
                End If
            End Try
            Return dtRes
        End Function

        Public Function ConsultaUsuarios(ByRef efCve As String) As DataTable
            Dim dtRes As New DataTable
            Dim sqlAdapter = New SqlDataAdapter("consWeb", _cnn)
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            sqlAdapter.SelectCommand.CommandTimeout = _timeout
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@opc", "usrEnt")
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm1", efCve)
            Try
                _cnn.Open()
                sqlAdapter.Fill(dtRes)
                sqlAdapter.Dispose()
                _cnn.Close()
            Catch ex As Exception
                Dim msg As String
                msg = "consWeb:entFin"
                'iErrorG = 3
                'agrega_err(iErrorG, msg, errores)
            Finally
                If _cnn.State = ConnectionState.Open Then
                    _cnn.Close()
                End If
            End Try
            Return dtRes
        End Function

        Public Function ValidaUsuario(ByRef efCve As String, ByRef usrCve As String, ByRef pass As String) As Int16
            Dim dtRes As New DataTable
            Dim valida As Int16 = 0
            Dim sqlAdapter = New SqlDataAdapter("consWeb", _cnn)
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            sqlAdapter.SelectCommand.CommandTimeout = _timeout
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@opc", "valUsr")
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm1", efCve)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm2", usrCve)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm3", pass)
            Try
                _cnn.Open()
                sqlAdapter.Fill(dtRes)
                If dtRes.Rows.Count > 0 Then
                    valida = Int16.Parse(dtRes.Rows(0).Item("valida").ToString())
                End If
                sqlAdapter.Dispose()
                _cnn.Close()
            Catch ex As Exception
                Dim msg As String
                msg = "consWeb:valUsr"
            Finally
                If _cnn.State = ConnectionState.Open Then
                    _cnn.Close()
                End If
            End Try
            Return valida
        End Function

        Public Function ConsultaDocumentos(ByRef efCve As String, ByRef usrCve As String) As DataTable
            Dim dtRes As New DataTable
            Dim sqlAdapter = New SqlDataAdapter("consWeb", _cnn)
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            sqlAdapter.SelectCommand.CommandTimeout = _timeout
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@opc", "docStd")
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm1", efCve)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm2", usrCve)
            Try
                _cnn.Open()
                sqlAdapter.Fill(dtRes)
                sqlAdapter.Dispose()
                _cnn.Close()
            Catch ex As Exception
                Dim msg As String
                msg = "consWeb:docStd"
                'iErrorG = 3
                'agrega_err(iErrorG, msg, errores)
            Finally
                If _cnn.State = ConnectionState.Open Then
                    _cnn.Close()
                End If
            End Try
            Return dtRes
        End Function

        Public Function ConsultaImagenes(ByRef efCve As String, ByRef tipDoc As String, ByRef numFol As Integer) As DataTable
            Dim dtRes As New DataTable
            Dim sqlAdapter = New SqlDataAdapter("consWeb", _cnn)
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            sqlAdapter.SelectCommand.CommandTimeout = _timeout
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@opc", "docImg")
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm1", efCve)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm2", tipDoc)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm3", "?")
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm4", numFol)
            Try
                _cnn.Open()
                sqlAdapter.Fill(dtRes)
                sqlAdapter.Dispose()
                _cnn.Close()
            Catch ex As Exception
                Dim msg As String
                msg = "consWeb:docImg"
                'iErrorG = 3
                'agrega_err(iErrorG, msg, errores)
            Finally
                If _cnn.State = ConnectionState.Open Then
                    _cnn.Close()
                End If
            End Try
            Return dtRes
        End Function

        Public Function AgregarImagen(ByRef efCve As String, ByRef tipDoc As String, ByRef numFol As Integer,
                                      ByRef consecutivo As Integer, ByRef usrCve As String, ByRef extension As String,
                                      ByRef archivo As String, ByRef fecha As String) As DataTable
            Dim dtRes As New DataTable
            Dim sqlAdapter = New SqlDataAdapter("consWeb", _cnn)
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            sqlAdapter.SelectCommand.CommandTimeout = _timeout
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@opc", "addImg")
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm1", efCve)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm2", tipDoc)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm3", consecutivo)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm4", numFol)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm5", usrCve)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm6", extension)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm7", archivo)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm8", fecha)

            Try
                _cnn.Open()
                sqlAdapter.Fill(dtRes)
                sqlAdapter.Dispose()
                _cnn.Close()
            Catch ex As Exception
                Dim msg As String
                msg = "consWeb:addImg"
                'iErrorG = 3
                'agrega_err(iErrorG, msg, errores)
            Finally
                If _cnn.State = ConnectionState.Open Then
                    _cnn.Close()
                End If
            End Try
            Return dtRes
        End Function


        Public Function BorrarImagen(ByRef efCve As String, ByRef tipDoc As String, ByRef numFol As Integer,
                                     ByRef consecutivo As Integer, ByRef usrCve As String,
                                     ByRef archivo As String) As DataTable
            Dim dtRes As New DataTable
            Dim sqlAdapter = New SqlDataAdapter("consWeb", _cnn)
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            sqlAdapter.SelectCommand.CommandTimeout = _timeout
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@opc", "delImg")
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm1", efCve)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm2", tipDoc)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm3", consecutivo)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm4", numFol)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm5", usrCve)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm7", archivo)

            Try
                _cnn.Open()
                sqlAdapter.Fill(dtRes)
                sqlAdapter.Dispose()
                _cnn.Close()
            Catch ex As Exception
                Dim msg As String
                msg = "consWeb:delImg"
                'iErrorG = 3
                'agrega_err(iErrorG, msg, errores)
            Finally
                If _cnn.State = ConnectionState.Open Then
                    _cnn.Close()
                End If
            End Try
            Return dtRes
        End Function


        Public Function ObtieneComboConfiguracion(ByRef tipDoc As String) As DataTable
            Dim dtRes As New DataTable
            Dim sqlAdapter = New SqlDataAdapter("consWeb", _cnn)
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            sqlAdapter.SelectCommand.CommandTimeout = _timeout
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@opc", "docCon")
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm1", "?")
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm2", tipDoc)
            Try
                _cnn.Open()
                sqlAdapter.Fill(dtRes)
                sqlAdapter.Dispose()
                _cnn.Close()
            Catch ex As Exception
                Dim msg As String
                msg = "consWeb:docCon"
                'iErrorG = 3
                'agrega_err(iErrorG, msg, errores)
            Finally
                If _cnn.State = ConnectionState.Open Then
                    _cnn.Close()
                End If
            End Try
            Return dtRes
        End Function

        Public Function ObtCboConfiguracion(ByRef efCve As String, ByRef tipDoc As String, ByRef numFol As Integer) As DataTable
            Dim dtRes As New DataTable
            Dim sqlAdapter = New SqlDataAdapter("consWeb", _cnn)
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            sqlAdapter.SelectCommand.CommandTimeout = _timeout
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@opc", "cboMul")
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm1", efCve)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm2", tipDoc)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm3", "?")
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm4", numFol)
            Try
                _cnn.Open()
                sqlAdapter.Fill(dtRes)
                sqlAdapter.Dispose()
                _cnn.Close()
            Catch ex As Exception
                Dim msg As String
                msg = "consWeb:docCon"
                'iErrorG = 3
                'agrega_err(iErrorG, msg, errores)
            Finally
                If _cnn.State = ConnectionState.Open Then
                    _cnn.Close()
                End If
            End Try
            Return dtRes
        End Function

        Public Function ObtCboBis(ByRef efCve As String, ByRef tipDoc As String, ByRef numFol As Integer) As DataTable
            Dim dtRes As New DataTable
            Dim sqlAdapter = New SqlDataAdapter("consWeb", _cnn)
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            sqlAdapter.SelectCommand.CommandTimeout = _timeout
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@opc", "cboMulPersonas")
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm1", efCve)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm2", tipDoc)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm3", "?")
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm4", numFol)
            Try
                _cnn.Open()
                sqlAdapter.Fill(dtRes)
                sqlAdapter.Dispose()
                _cnn.Close()
            Catch ex As Exception
                Dim msg As String
                msg = "consWeb:docCon"
                'iErrorG = 3
                'agrega_err(iErrorG, msg, errores)
            Finally
                If _cnn.State = ConnectionState.Open Then
                    _cnn.Close()
                End If
            End Try
            Return dtRes
        End Function



        'Public Function ValidaExisteFolio(ByRef efCve As String, ByRef tipDoc As String, ByRef numFol As Integer) As Integer
        '    Dim dtRes As New DataTable
        '    Dim iExiste As Integer
        '    Dim sqlAdapter = New SqlDataAdapter("consWeb", _cnn)
        '    sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        '    sqlAdapter.SelectCommand.CommandTimeout = _timeout
        '    sqlAdapter.SelectCommand.Parameters.AddWithValue("@opc", "valFol")
        '    sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm1", efCve)
        '    sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm2", tipDoc)
        '    sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm3", "?")
        '    sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm4", numFol)
        '    Try
        '        _cnn.Open()
        '        sqlAdapter.Fill(dtRes)


        '        If dtRes.Rows.Count > 0 Then
        '            iExiste = CType(dtRes.Rows(0).Item("existe"), Integer)
        '        End If

        '        dtRes.Dispose()
        '        sqlAdapter.Dispose()
        '        _cnn.Close()
        '    Catch ex As Exception
        '        Dim msg As String
        '        msg = "consWeb:valFol"
        '        'iErrorG = 3
        '        'agrega_err(iErrorG, msg, errores)
        '    Finally
        '        If _cnn.State = ConnectionState.Open Then
        '            _cnn.Close()
        '        End If
        '    End Try
        '    Return iExiste
        'End Function


        Public Function ValidaExisteFolio(ByRef efCve As String, ByRef tipDoc As String, ByRef numFol As Integer) As DataTable
            Dim dtRes As New DataTable
            'Dim iExiste As Integer
            Dim sqlAdapter = New SqlDataAdapter("consWeb", _cnn)
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            sqlAdapter.SelectCommand.CommandTimeout = _timeout
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@opc", "valFol")
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm1", efCve)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm2", tipDoc)
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm3", "?")
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@prm4", numFol)
            Try
                _cnn.Open()
                sqlAdapter.Fill(dtRes)
                dtRes.Dispose()
                sqlAdapter.Dispose()
                _cnn.Close()
            Catch ex As Exception
                Dim msg As String
                msg = "consWeb:valFol"
                'iErrorG = 3
                'agrega_err(iErrorG, msg, errores)
            Finally
                If _cnn.State = ConnectionState.Open Then
                    _cnn.Close()
                End If
            End Try
            Return dtRes
        End Function


        Public Function ConsultaConfiguraciones() As DataTable
            Dim dtRes As New DataTable
            Dim sqlAdapter = New SqlDataAdapter("consWeb", _cnn)
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            sqlAdapter.SelectCommand.CommandTimeout = _timeout
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@opc", "config")
            Try
                _cnn.Open()
                sqlAdapter.Fill(dtRes)
                sqlAdapter.Dispose()
                _cnn.Close()
            Catch ex As Exception
                Dim msg As String
                msg = "consWeb:config"
            Finally
                If _cnn.State = ConnectionState.Open Then
                    _cnn.Close()
                End If
            End Try
            Return dtRes
        End Function

    End Class

End Namespace