using BTLW.Models;

namespace BTLW.Repository
{
    public interface INuocSanXuatRepository
    {
        NuocSx Add(NuocSx nuocSx);

        NuocSx Update(NuocSx nuocSx);

        NuocSx Delete(string maNuocSx);

        NuocSx GetNuocSx(string maNuocSx);

        IEnumerable<NuocSx> GetAllNuocSx();
    }
}
