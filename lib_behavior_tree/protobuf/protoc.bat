@echo off

for %%1 in (*.proto) do (
@echo %%1
@echo %%~n1
..\..\3rd\bin\protoc.exe %%1 --cpp_out=.\
..\..\3rd\bin\ProtoGen\protogen.exe -i:%%1 -o:..\..\ai_editor\%%~n1.cs
)

@echo protoc±‡“ÎÕÍ≥…
pause
