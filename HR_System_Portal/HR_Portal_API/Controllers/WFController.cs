using AutoMapper;

using Microsoft.AspNetCore.Mvc;

namespace HR_Portal_API.Controllers;

public class WFController : ControllerBase
{
    private readonly IMapper mapper;
    public WFController(IMapper mapper)
    {
        this.mapper = mapper;
    }
}
