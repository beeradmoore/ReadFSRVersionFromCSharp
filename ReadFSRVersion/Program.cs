using ReadFSRVersion;

var httpClient = new HttpClient();

var dllEntries = new List<DLLEntry>();

dllEntries.Add(new DLLEntry("v2.2.0", "Kits/FidelityFX/signedbin/amd_fidelityfx_denoiser_dx12.dll"));
dllEntries.Add(new DLLEntry("v2.2.0", "Kits/FidelityFX/signedbin/amd_fidelityfx_framegeneration_dx12.dll"));
dllEntries.Add(new DLLEntry("v2.2.0", "Kits/FidelityFX/signedbin/amd_fidelityfx_loader_dx12.dll"));
dllEntries.Add(new DLLEntry("v2.2.0", "Kits/FidelityFX/signedbin/amd_fidelityfx_radiancecache_dx12.dll"));
dllEntries.Add(new DLLEntry("v2.2.0", "Kits/FidelityFX/signedbin/amd_fidelityfx_upscaler_dx12.dll"));

dllEntries.Add(new DLLEntry("v2.1.1", "Kits/FidelityFX/signedbin/amd_fidelityfx_denoiser_dx12.dll"));
dllEntries.Add(new DLLEntry("v2.1.1", "Kits/FidelityFX/signedbin/amd_fidelityfx_framegeneration_dx12.dll"));
dllEntries.Add(new DLLEntry("v2.1.1", "Kits/FidelityFX/signedbin/amd_fidelityfx_loader_dx12.dll"));
dllEntries.Add(new DLLEntry("v2.1.1", "Kits/FidelityFX/signedbin/amd_fidelityfx_radiancecache_dx12.dll"));
dllEntries.Add(new DLLEntry("v2.1.1", "Kits/FidelityFX/signedbin/amd_fidelityfx_upscaler_dx12.dll"));

dllEntries.Add(new DLLEntry("v2.1.0", "Kits/FidelityFX/signedbin/amd_fidelityfx_denoiser_dx12.dll"));
dllEntries.Add(new DLLEntry("v2.1.0", "Kits/FidelityFX/signedbin/amd_fidelityfx_framegeneration_dx12.dll"));
dllEntries.Add(new DLLEntry("v2.1.0", "Kits/FidelityFX/signedbin/amd_fidelityfx_loader_dx12.dll"));
dllEntries.Add(new DLLEntry("v2.1.0", "Kits/FidelityFX/signedbin/amd_fidelityfx_radiancecache_dx12.dll"));
dllEntries.Add(new DLLEntry("v2.1.0", "Kits/FidelityFX/signedbin/amd_fidelityfx_upscaler_dx12.dll"));

dllEntries.Add(new DLLEntry("v2.0.0", "Kits/FidelityFX/signedbin/amd_fidelityfx_framegeneration_dx12.dll"));
dllEntries.Add(new DLLEntry("v2.0.0", "Kits/FidelityFX/signedbin/amd_fidelityfx_loader_dx12.dll"));
dllEntries.Add(new DLLEntry("v2.0.0", "Kits/FidelityFX/signedbin/amd_fidelityfx_upscaler_dx12.dll"));

dllEntries.Add(new DLLEntry("v1.1.4", "PrebuiltSignedDLL/amd_fidelityfx_dx12.dll"));
dllEntries.Add(new DLLEntry("v1.1.4", "PrebuiltSignedDLL/amd_fidelityfx_vk.dll"));

dllEntries.Add(new DLLEntry("v1.1.3", "PrebuiltSignedDLL/amd_fidelityfx_dx12.dll"));
dllEntries.Add(new DLLEntry("v1.1.3", "PrebuiltSignedDLL/amd_fidelityfx_vk.dll"));

dllEntries.Add(new DLLEntry("v1.1.2", "PrebuiltSignedDLL/amd_fidelityfx_dx12.dll"));
dllEntries.Add(new DLLEntry("v1.1.2", "PrebuiltSignedDLL/amd_fidelityfx_vk.dll"));

dllEntries.Add(new DLLEntry("v1.1.1", "PrebuiltSignedDLL/amd_fidelityfx_dx12.dll"));
dllEntries.Add(new DLLEntry("v1.1.1", "PrebuiltSignedDLL/amd_fidelityfx_vk.dll"));

dllEntries.Add(new DLLEntry("v1.1.0", "PrebuiltSignedDLL/amd_fidelityfx_dx12.dll"));
dllEntries.Add(new DLLEntry("v1.1.0", "PrebuiltSignedDLL/amd_fidelityfx_vk.dll"));

// There are no FSR DLLs in tags v1.0.0, fsr3-v3.0.4, or fsr3-v3.0.3


foreach (var dllEntry in dllEntries)
{
    await DownloadDllEntryAsync(dllEntry);
}

Console.WriteLine();

var amdFidelityFXAPI = new AMDFidelityFXAPI();

foreach (var dllEntry in dllEntries)
{
    amdFidelityFXAPI.GetVersions(dllEntry.DLLPath, dllEntry.DLLType);
    Console.WriteLine();
}

async Task DownloadDllEntryAsync(DLLEntry dllEntry)
{
    if (File.Exists(dllEntry.DLLPath))
    {
        return;
    }

    var fileName = Path.GetFileName(dllEntry.SourcePath);
    Console.WriteLine($"Downloading {dllEntry.Tag} - {fileName}");

    using (var response = await httpClient.GetAsync(dllEntry.Url, HttpCompletionOption.ResponseHeadersRead))
    {
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            Console.WriteLine($"Could not download: {response.StatusCode}");
            return;
        }

        if (Directory.Exists(dllEntry.OutPath) == false)
        {
            Directory.CreateDirectory(dllEntry.OutPath);
        }

        using (var stream = await response.Content.ReadAsStreamAsync())
        {

            using (var fileStream = File.Create(dllEntry.DLLPath))
            {
                await stream.CopyToAsync(fileStream);
            }
        }
    }
}