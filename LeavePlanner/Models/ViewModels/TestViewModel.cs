using AutoMapper;
using LeavePlanner.Core;
using LeavePlanner.Core.Models.Test;
using LeavePlanner.Models.DTOs;

namespace LeavePlanner.Models.ViewModels
{
    public class TestViewModel
    {
        public List<TestDto> TestDtos { get; set; }

        public TestViewModel()
        {
            TestDtos = new List<TestDto>();
        }

        public async Task PrepareInitialData(IUnitOfWork unitOfWork, IMapper mapper)
        {
            var testData = await unitOfWork.TestRepository.GetTestDataAsync();
            TestDtos = mapper.Map<List<TestTable>, List<TestDto>>(testData);
        }
    }
}
