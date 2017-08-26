# FuzzyCore
remote control
<b><h2>Example Print Message</h2></b>
```json
{
  "CommandType"   : "print_message",
  "FormCaption"   : "Test Form",
  "Text"          : "Hello World",
  "FontSize"      : 50
}
```

<b><h2>Example: Print Message After 5 seconds</h2></b>
```json
{
  "CommandType"   : "print_message",
  "FormCaption"   : "Test Form",
  "Text"          : "Hello World",
  "FontSize"      : 50,
  "AfterTime"     : 5000
}
```
<b><h2>Example: Open Program</h2></b>
```json
{
  "CommandType"   : "open_program",
  "Text"          : "Spotify"
}
```
<b><h2>Example: Open Program After 5 seconds</h2></b>
```json
{
  "CommandType"   : "open_program",
  "Text"          : "Spotify",
  "AfterTime"     : "5000"
}
```
<b><h2>Example: Open Program and Destroy 5 seconds later</h2></b>
```json
{
  "CommandType"   : "open_program",
  "Text"          : "Spotify",
  "OverTime"      : "5000"
}
```
<b><h2>Example: List&View -> Files And Folders</h2></b>
```json
{
  "CommandType"   : "get_folder_list",
  "FilePath"      : "c:\\users"
}
```
<b><h2>Example: Get File</h2></b>
```json
{
  "CommandType"   : "get_file",
  "FilePath"      : "c:\\users",
  "Text"          : "desktop.ini"
}
```
