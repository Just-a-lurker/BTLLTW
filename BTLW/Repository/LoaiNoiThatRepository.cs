using BTLW.Models;

namespace BTLW.Repository
{
    public class LoaiNoiThatRepository : ILoaiNoiThatRepository
    {
        private readonly Lttqnhom6Context _context;

        public LoaiNoiThatRepository(Lttqnhom6Context context)
        {
            _context = context;
        }

        public TheLoai Add(TheLoai theLoai)
        {
            _context.TheLoais.Add(theLoai);
            _context.SaveChanges();
            return theLoai;
        }

        public TheLoai Delete(string maLoai)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TheLoai> GetAllTheLoai()
        {
            return _context.TheLoais;
        }

        public TheLoai GetTheLoai(string maLoai)
        {
            return _context.TheLoais.Find("maLoai");
        }

        public TheLoai Update(TheLoai theLoai)
        {
            _context.Update(theLoai);
            _context.SaveChanges();
            return theLoai;
        }
    }
}
