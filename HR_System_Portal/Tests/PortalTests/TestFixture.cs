using HR_Portal_API;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTests;

public class TestFixture : IDisposable
{
    private readonly WebApplicationBuilder? builder;
    public IServiceProvider ServiceProvider { get; }

    public TestFixture()
    {
        ServiceProvider = Configuration.ConfigServices(builder!)
                            .BuildServiceProvider();
    }

    public void Dispose() { }
}
