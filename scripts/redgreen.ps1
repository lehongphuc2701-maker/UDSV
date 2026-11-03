# Chạy lại chu trình "đỏ → xanh" của luật trần tín chỉ (dùng cho ảnh chụp ở Chương 3).
#   .\redgreen.ps1            # chạy bản hiện tại (kỳ vọng: tất cả xanh)
#   .\redgreen.ps1 -Red       # tạm bỏ luật, chạy để thấy ĐỎ, rồi khôi phục file
#
# LƯU Ý: phải build lại từ đầu (-t:Rebuild) sau khi sửa file, nếu không dotnet dùng
# bản biên dịch cũ và kết quả sẽ sai (đã gặp đúng lỗi này lần chạy đầu).
param([switch]$Red)

$ErrorActionPreference = 'Stop'
$goc = "C:\Users\canhh\Documents\deepseek-harness\default-workspace"
$bll = Join-Path $goc "UDSV\UDSV\UDSV.BLL\ChuongTrinhBLL.cs"
$proj = Join-Path $goc "_testcheck\_testcheck.csproj"

function ChayTest {
    & dotnet build $proj -t:Rebuild -v q --nologo | Out-Null
    & dotnet run --project $proj --no-build 2>&1 |
        Select-String -Pattern "^(---|   (PASS|FAIL)|TỔNG)"
}

if ($Red) {
    $saoLuu = "$bll.bak"
    Copy-Item $bll $saoLuu -Force
    try {
        $t = Get-Content $bll -Raw -Encoding UTF8
        $t = $t -replace 'if \(hienTai \+ tinChi\.Value > tran\)', 'if (false && hienTai + tinChi.Value > tran)'
        Set-Content $bll -Value $t -Encoding UTF8 -NoNewline

        Write-Host "=== CHẠY KHI CHƯA CÓ LUẬT (kỳ vọng: 1 bài ĐỎ) ===" -ForegroundColor Yellow
        ChayTest
    }
    finally {
        Move-Item $saoLuu $bll -Force
        Write-Host "=== đã khôi phục ChuongTrinhBLL.cs ===" -ForegroundColor Green
    }
}
else {
    Write-Host "=== CHẠY KHI ĐÃ CÓ LUẬT (kỳ vọng: tất cả XANH) ===" -ForegroundColor Green
    ChayTest
}
