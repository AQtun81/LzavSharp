param($target)

New-Item -Force -ItemType directory -Path src/runtimes/$target/native | Out-Null

# Install compiler if not already present
switch ($target)
{
  "linux-arm"   { sudo apt-get install -y gcc-arm-linux-gnueabihf }
  "linux-arm64" {  }
  "linux-x64"   {  }
  "win-arm64"   {  }
  "win-x64"     {  }
  "win-x86"     {  }
  "osx-arm64"   {  }
  "osx-x64"     {  }
}

# Build
switch ($target)
{
  "linux-arm"   { arm-linux-gnueabihf-gcc           -shared -o src/runtimes/linux-arm/native/lzav.so    temp/lzav.c -O2 }
  "linux-arm64" { aarch64-linux-gnu-gcc             -shared -o src/runtimes/linux-arm64/native/lzav.so  temp/lzav.c -O2 }
  "linux-x64"   { x86_64-linux-gnu-gcc              -shared -o src/runtimes/linux-x64/native/lzav.so    temp/lzav.c -O2 }
  "osx-arm64"   { clang -target arm64-apple-darwin  -shared -o src/runtimes/osx-arm64/native/lzav.dylib temp/lzav.c -O2 }
  "osx-x64"     { clang -target x86_64-apple-darwin -shared -o src/runtimes/osx-x64/native/lzav.dylib   temp/lzav.c -O2 }
  {$_ -like "win-*"} {
    $vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
    $vsPath = & $vswhere -latest -products * -requires Microsoft.VisualStudio.Component.VC.Tools.x86.x64 -property installationPath
    $vcVarsPath = "$vsPath\VC\Auxiliary\Build\vcvarsall.bat"
    $arch = $target -replace 'win-', ''
    cmd.exe /c "`"$vcVarsPath`" $arch && cl /LD /O2 temp\lzav.c /link /OUT:src\runtimes\$target\native\lzav.dll"
  }
}