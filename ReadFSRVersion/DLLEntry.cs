using System;
using System.Collections.Generic;
using System.Text;

namespace ReadFSRVersion;

internal class DLLEntry
{
    public string Name => Path.GetFileName(SourcePath);
    public string Tag { get; init;  } = string.Empty;
    public ulong DLLType { get; init; }
    public string SourcePath { get; init; } = string.Empty;
    public string OutPath => Path.Combine(Tag, Path.GetDirectoryName(SourcePath) ?? string.Empty);
    public string DLLPath => Path.Combine(OutPath, Name);
    public string Url => $"https://raw.githubusercontent.com/GPUOpen-LibrariesAndSDKs/FidelityFX-SDK/refs/tags/{Tag}/{SourcePath}";

    public DLLEntry(string tag, string expectedPath)
    {
        Tag = tag;
        SourcePath = expectedPath;

        var fileName = Path.GetFileName(SourcePath);
        DLLType = fileName switch {
            "amd_fidelityfx_denoiser_dx12.dll" => FxxConsts.FFX_API_EFFECT_ID_DENOISER,
            "amd_fidelityfx_framegeneration_dx12.dll" => FxxConsts.FFX_API_EFFECT_ID_FRAMEGENERATION,
            "amd_fidelityfx_loader_dx12.dll" => FxxConsts.FFX_API_EFFECT_ID_UPSCALE,
            "amd_fidelityfx_radiancecache_dx12.dll" => FxxConsts.FFX_API_EFFECT_ID_RADIANCECACHE,
            "amd_fidelityfx_upscaler_dx12.dll" => FxxConsts.FFX_API_EFFECT_ID_UPSCALE,
            "amd_fidelityfx_vk.dll" => FxxConsts.FFX_API_EFFECT_ID_UPSCALE,
            "amd_fidelityfx_dx12.dll" => FxxConsts.FFX_API_EFFECT_ID_UPSCALE,
            _ => throw new Exception("DLL is not what was expected."),
        };
    }
}
