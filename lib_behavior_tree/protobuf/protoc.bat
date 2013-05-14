@echo off

for %%1 in (*.proto) do (
@echo %%1
..\..\3rd\bin\protoc.exe %%1 --cpp_out=.\
)

@echo protoc±‡“ÎÕÍ≥…