using BTLW.Models;

namespace BTLW.Repository
{
    public class NuocSanXuatRepository : INuocSanXuatRepository
    {
        private readonly Lttqnhom6Context _context;

        public NuocSanXuatRepository(Lttqnhom6Context context)
        {
            _context = context;
        }

        public NuocSx Add(NuocSx nuocSx)
        {
            _context.NuocSxes.Add(nuocSx);
            _context.SaveChanges();
            return nuocSx;
        }

        public NuocSx Delete(string maNuocSx)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NuocSx> GetAllNuocSx()
        {
            return _context.NuocSxes;
        }

        public NuocSx GetNuocSx(string maNuocSx)
        {
            return _context.NuocSxes.Find("maNuocSx");
        }

        public NuocSx Update(NuocSx nuocSx)
        {
            _context.Update(nuocSx);
            _context.SaveChanges();
            return nuocSx;
        }
    }
}
