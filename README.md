# IISKeepAlive
Small application that stops IIS from recycling/idling websites by making http requests every few minutes. Meant to be used as a wrapped windows service using [winsw](https://github.com/winsw/winsw)

## Usage
Create a file called Urls.json that contains an array of urls
```
Urls.json
["localhost", "localhost:80"]
```
then open a command console with as a admin and type
```
IISKeepAlive.ServiceWrapper.exe install
IISKeepAlive.ServiceWrapper.exe start
```

## Arguments
The file IISKeepAlive.ServiceWrapper.xml has an argument section that can be used to apply the following arguments
```
-path=[pathToFileThatContainsTheUrls: string]
-interval=[intervalBetweenRequestsInMinutes: int]
-logResults=[logTheRequestResultsToALogFile: bool]
-logPath=[pathToLogFile: string]
```
