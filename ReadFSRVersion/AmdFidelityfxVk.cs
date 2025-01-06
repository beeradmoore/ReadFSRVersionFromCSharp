using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ReadFSRVersion;


public class AmdFidelityfxVk
{
    [DllImport("amd_fidelityfx_vk.dll", CallingConvention = CallingConvention.Cdecl)]
    //public static extern FfxApiReturnCodes ffxQuery(IntPtr context, IntPtr header);
    //public static extern FfxApiReturnCodes ffxQuery(IntPtr context, ref QueryDescGetVersions header);
    public static extern FfxApiReturnCodes ffxQuery(IntPtr context, ref ffxApiHeader header);


    public string GetVersion()
    {
        /*
        ffx::QueryDescGetVersions versionQuery{};
        versionQuery.createDescType = FFX_API_CREATE_CONTEXT_DESC_TYPE_UPSCALE;
        versionQuery.device = GetDX12Device(); // only for DirectX 12 applications
        uint64_t versionCount = 0;
        versionQuery.outputCount = &versionCount;
        // get number of versions for allocation
        ffxQuery(nullptr, &versionQuery.header);

        std::vector<const char*> versionNames;
        std::vector<uint64_t> versionIds;
        m_FsrVersionIds.resize(versionCount);
        versionNames.resize(versionCount);
        versionQuery.versionIds = versionIds.data();
        versionQuery.versionNames = versionNames.data();
        // fill version ids and names arrays.
        ffxQuery(nullptr, &versionQuery.header);
        ```*/

        //ffx::QueryDescGetVersions versionQuery{};
        var versionQuery = new QueryDescGetVersions();

        //versionQuery.createDescType = FFX_API_CREATE_CONTEXT_DESC_TYPE_UPSCALE;
        versionQuery.createDescType = FxxConsts.FFX_API_CREATE_CONTEXT_DESC_TYPE_UPSCALE;

        // versionQuery.device = GetDX12Device(); // only for DirectX 12 applications
        versionQuery.device = IntPtr.Zero;

        // uint64_t versionCount = 0;
        UInt64 versionCount = 0;

        // versionQuery.outputCount = &versionCount;
        GCHandle versionCountHandle = GCHandle.Alloc(versionCount, GCHandleType.Pinned);
        versionQuery.outputCount = versionCountHandle.AddrOfPinnedObject();

        //GCHandle versionQueryHandle = GCHandle.Alloc(versionQuery, GCHandleType.Pinned);
        GCHandle headerHandle = GCHandle.Alloc(versionQuery.header, GCHandleType.Pinned);

        try
        {
            Console.WriteLine("VK - Reading version count");
            // get number of versions for allocation
            // ffxQuery(IntPtr.Zero, &versionQuery.header);


            //var returnCode = ffxQuery(IntPtr.Zero, headerHandle.AddrOfPinnedObject());
            //var returnCode = ffxQuery(IntPtr.Zero, ref versionQuery);
            var returnCode = ffxQuery(IntPtr.Zero, ref versionQuery.header);

            Console.WriteLine($"VK - ReturnCode: {returnCode}");
            Debugger.Break();
        }
        finally
        {
            versionCountHandle.Free();
            //versionQueryHandle.Free();
            //headerHandle.Free();
            Debugger.Break();
        }

        return string.Empty;
    }
}
