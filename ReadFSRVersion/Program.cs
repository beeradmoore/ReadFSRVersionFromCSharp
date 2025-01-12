
using ReadFSRVersion;

var fsrVkDll = "amd_fidelityfx_vk.dll";
var fsrDx12Dll = "amd_fidelityfx_dx12.dll";
var fsrVkDllUrl = "https://raw.githubusercontent.com/GPUOpen-LibrariesAndSDKs/FidelityFX-SDK/refs/heads/main/PrebuiltSignedDLL/amd_fidelityfx_vk.dll";
var fsrDx12DllUrl = "https://raw.githubusercontent.com/GPUOpen-LibrariesAndSDKs/FidelityFX-SDK/refs/heads/main/PrebuiltSignedDLL/amd_fidelityfx_dx12.dll";

async Task DownloadDllAsync(string name, string url)
{
    Console.WriteLine($"Downloading {name}");
    var httpClient = new HttpClient();
    using (var stream = await httpClient.GetStreamAsync(url))
    {
        using (var fileStream = File.Create(name))
        {
            stream.CopyTo(fileStream);
        }
    }
}

if (File.Exists(fsrVkDll) == false)
{
    await DownloadDllAsync(fsrVkDll, fsrVkDllUrl);
}

if (File.Exists(fsrDx12Dll) == false)
{
    await DownloadDllAsync(fsrDx12Dll, fsrDx12DllUrl);
}


//var amdFidelityfxVk = new AmdFidelityfxVk();
//amdFidelityfxVk.GetVersion();

var amdFidelityFXAPI = new AMDFidelityFXAPI();
amdFidelityFXAPI.GetVersions(fsrVkDll);
amdFidelityFXAPI.GetVersions(fsrDx12Dll);