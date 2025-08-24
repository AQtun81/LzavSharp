New-Item -Force -ItemType directory -Path temp | Out-Null

(Get-Content -Path "lzav_macro.h") | Out-File -FilePath "temp/lzav.h" -Encoding UTF8
((Get-Content -Path "lzav_src/lzav.h") -replace '^[ \t]*#define LZAV_INLINE.*$', '') | Out-File -FilePath "temp/lzav.h" -Encoding UTF8 -Append
("#include `"lzav.h`"") | Out-File -FilePath "temp/lzav.c" -Encoding UTF8