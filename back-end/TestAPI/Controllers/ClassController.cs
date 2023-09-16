using Microsoft.AspNetCore.Mvc;
using System;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly ILogger<ClassController> _logger;

        public ClassController(ILogger<ClassController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(Data.DanhSachLop);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = Data.DanhSachLop.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return NotFound("Không tìm thấy lớp này");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = Data.DanhSachLop.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return NotFound("Không tìm thấy lớp này");
            }
            if(Data.DanhSachSV.FirstOrDefault(x=>x.ClassId == id) != null)
            {
                return BadRequest("Lớp này vẫn còn sinh viên, không thể xóa");
            }

            Data.DanhSachLop.Remove(result);
            return Ok("Đã xóa lớp thành công");
        }

        [HttpPost()]
        public async Task<IActionResult> Create(Lop lop)
        {
            var result = Data.DanhSachLop.FirstOrDefault(x => x.Code == lop.Code);
            if (result == null)
            {

                int newID = 0;
                if (Data.DanhSachLop.Count() > 0)
                {
                    newID = Data.DanhSachLop.Max(x => x.Id) +1;
                }
                lop.Id = newID;
                Data.DanhSachLop.Add(lop);
                return Ok("Thêm thành công");
            }
            
            return BadRequest($"Mã lớp {lop.Code} đã tồn tại");
        }

        [HttpPut()]
        public async Task<IActionResult> Update(Lop lop)
        {

            var result = Data.DanhSachLop.FirstOrDefault(x => x.Id == lop.Id);
            if (result == null)
            {
                return NotFound("Không tìm thấy lớp này");
            }

            if(Data.DanhSachLop.FirstOrDefault(x => x.Code == lop.Code && x.Id != lop.Id) != null)
            {
                return BadRequest($"Mã lớp {lop.Code} đã tồn tại");
            }

            result.Name = lop.Name;
            result.Code = lop.Code;
            return Ok("Cập nhật thông tin lớp thành công");
        }
    }
}