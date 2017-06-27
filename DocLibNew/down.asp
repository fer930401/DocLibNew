<%

Response.addHeader "pragma", "no-cache"
Response.CacheControl = "Private"

Response.ExpiresAbsolute = "1/5/2000 12:12:12"
'Response.Buffer=true
Response.Clear() 
Server.ScriptTimeOut = 28800

Dim sDownloadPath 
Dim ruta_lib 

ruta_lib = request.QueryString("map")  & request.QueryString("arch") 
'response.write(ruta_lib)
Response.ContentType = "application/x-zip-compressed"
Response.Redirect(ruta_lib)
'Response.write "<html><sc"&"ript>window.close();</s"&"cript></html>"
'Response.end


%>
