        Dim newWebKey As String = "CHANGED@CHANGED" 'Placeholder, get new web key from text field!

        Dim pattern As Regex = New Regex(".*\[(.*webkey"":)(.*)(,""lineItemId.*)\].*")
        Dim match As Match = pattern.Match(input)
        Dim toSend As String = ""
        If (match.Success) Then
        'first drop quotes in regex and then manually add quotes, because if null the webkey does not have quotes!
            toSend = match.Groups(1).ToString() + """" + newWebKey + """" + match.Groups(3).ToString()
        Else
            'Throw Error: Regular Expression did not match input
        End If


        'Convert object to bytes
        Dim jsondata As Byte() = Encoding.UTF8.GetBytes(toSend)

        'set up webrequest
        Dim request As WebRequest
        request = WebRequest.Create("http://localhost:8080/echo") 'https://go.industrysoftware.automation.siemens.com/c2o/assignment/v2/assign")
        request.Method = "POST"
        request.ContentLength = jsondata.Length
        request.ContentType = "application/json"

        'send data
        Dim requestStream As Stream = request.GetRequestStream
        requestStream.Write(jsondata, 0, jsondata.Length)
        requestStream.Close()

        'receive answer
        Dim responseStream As Stream = request.GetResponse.GetResponseStream
        Dim responseReader As New StreamReader(responseStream)
        Dim response As String = responseReader.ReadToEnd

        'Do stuff with response
        'Console.WriteLine("response: " + response)
