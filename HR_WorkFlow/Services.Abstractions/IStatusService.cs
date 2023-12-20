using Services.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IStatusService
    {
        public Task<Guid> Create(StatusCreateRequest request);

        public Task<StatusResponse> GetById(Guid id);   
        public Task<IEnumerable<StatusResponse>> GetAll();
        public Task<StatusResponse> Update(StatusEditRequest request);
        public Task<bool> Delete(Guid id);
    }
}
