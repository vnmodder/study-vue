using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<ClassController> _logger;

        public StudentController(ILogger<ClassController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(Data.DanhSachSV);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = Data.DanhSachSV.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return NotFound("Không tìm thấy sinh viên này");
            }

            return Ok(result);
        }

        [HttpGet("Class/{id}")]
        public async Task<IActionResult> GetByClassId(int id)
        {
            return Ok(Data.DanhSachSV.Where(x => x.ClassId == id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = Data.DanhSachSV.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return NotFound("Không tìm thấy sinh viên này");
            }

            Data.DanhSachSV.Remove(result);
            return Ok("Đã xóa sinh viên thành công");
        }

        [HttpPost()]
        public async Task<IActionResult> Create(SinhVien sv)
        {

            var result = Data.DanhSachSV.FirstOrDefault(x => x.Code == sv.Code);
            if (result == null)
            {
                if (Data.DanhSachLop.FirstOrDefault(x => x.Id == sv.ClassId) == null)
                {
                    return BadRequest($"Lớp với mã {sv.ClassId} không tồn tại");
                }

                int newID = 0;
                if (Data.DanhSachSV.Count() > 0)
                {
                    newID = Data.DanhSachSV.Max(x => x.Id) + 1;
                }

                sv.Id = newID;
                Data.DanhSachSV.Add(sv);
                return Ok("Thêm mới sinh viên thành công");
            }

            return BadRequest($"Mã sinh viên {sv.Code} đã tồn tại");
        }

        [HttpPut()]
        public async Task<IActionResult> Update(SinhVien sv)
        {

            var result = Data.DanhSachSV.FirstOrDefault(x => x.Id == sv.Id);
            if (result == null)
            {
                return NotFound("Không tìm thấy sinh viên này");
            }

            if (Data.DanhSachLop.FirstOrDefault(x=>x.Id == sv.ClassId)==null)
            {
                return BadRequest($"Lớp với mã {sv.ClassId} không tồn tại");
            }

            if (Data.DanhSachSV.FirstOrDefault(x => x.Code == sv.Code && x.Id != sv.Id) != null)
            {
                return BadRequest($"Mã sinh viên {sv.Code} đã tồn tại");
            }

            result.Name = sv.Name;
            result.ClassId = sv.ClassId;
            result.Code = sv.Code;
            return Ok("Cập nhật thông tin sinh viên thành công");
        }
    }
}
