# ReadFSRVersionFromCSharp

This repo is an attempt to load FSR version from AMD FidelityFX dll from C#. It is a port of C++ code found [here](https://gpuopen.com/manuals/fidelityfx_sdk/fidelityfx_sdk-page_ffx-api/#version-selection).

It is possible that my understanding of the above code is not leading to expected results.


## Goal

I'd like this to be able to read the FSR version provided by a dll for use in [DLSS Swapper](https://github.com/beeradmoore/dlss-swapper). Currently dll product versions do not match known version numbers. 

For example the latest `amd_fidelityfx_dx12.dll` has the following properties,

| Property        | Value       |
|-----------------|-------------|
| File version    | 1.0.1.39157 |
| Product version | 1.0.1.0     |

However it is know this is FSR 3.1.3 from the FidelityFX SDK v1.1.3.

This can be shown with the following steps:
1. Downloading [FidelityFX SDK v1.1.3](https://github.com/GPUOpen-LibrariesAndSDKs/FidelityFX-SDK/releases/tag/v1.1.3),
2. Extracting it
3. Running `UpdateMedia.bat` to load media assets
4. Running `bin/FFX_API_FSR_DX12.exe` and observing the `FSR Version` in the top left being `3.1.3`.

To then confirm this is loading version number from the dll you can:
1. Downloading [FidelityFX SDK v1.1.1](https://github.com/GPUOpen-LibrariesAndSDKs/FidelityFX-SDK/releases/tag/v1.1.1),
2. Extracting it
3. Copying `PrebuiltSignedDLL/amd_fidelityfx_dx12.dll` to the `bin/` folder of the first test where `3.1.3` was observed.
4. Running `bin/FFX_API_FSR_DX12.exe` again and note that `FSR Version` in the top left has changed to `3.1.1`.

While this is a reliable way to test FSR version, I'd like to do it in a standalone project.

### Current attempt

The project `ReadFSRVersion` I have created so far will download the latest `amd_fidelityfx_vk.dll` and `amd_fidelityfx_dx12.dll` from the AMD FidelityFX-SDK repository [here](https://github.com/GPUOpen-LibrariesAndSDKs/FidelityFX-SDK/). These are both `3.1.3`.

It will then use the ported code from [version selection](https://gpuopen.com/manuals/fidelityfx_sdk/fidelityfx_sdk-page_ffx-api/#version-selection) to load FSR versions dynamically. This code is also similar to what is found in the [FFX samples](https://github.com/GPUOpen-LibrariesAndSDKs/FidelityFX-SDK/blob/main/samples/fsrapi/fsrapirendermodule.cpp#L432).


### Current results

Currently calling `ffxQuery` returns `FFX_API_RETURN_OK`, however `versionCount` remains as its initial value (even if you change the initial value).

I am sure the call itself is working because if you change the header type (`++versionQuery.header.type;`) the return code will be `FFX_API_RETURN_NO_PROVIDER`.

I am unsure if I am passing structs to C++ dll incorrectly or if what I am attempting to do does not work like this. I don't know enough C++ to create a raw C++ minimal example, but if you can feel free to send a PR.