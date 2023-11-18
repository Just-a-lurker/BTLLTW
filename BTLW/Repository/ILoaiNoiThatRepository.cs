using BTLW.Models;

namespace BTLW.Repository
{
    public interface ILoaiNoiThatRepository
    {
        TheLoai Add(TheLoai theLoai);

        TheLoai Update(TheLoai theLoai);

        TheLoai Delete(string maLoai);

        TheLoai GetTheLoai(string maLoai);

        IEnumerable<TheLoai> GetAllTheLoai();
    }
}
