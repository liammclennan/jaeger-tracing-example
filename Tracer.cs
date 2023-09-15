using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace GettingStarted;

public class TraceThing
{
    public void Go()
    {
        using var tracerProvider = Sdk.CreateTracerProviderBuilder()
            .AddSource("Service F")
            .ConfigureResource(resource => resource.AddService(
                serviceName: "Service F",
                serviceVersion: "0.0.1"))
            .AddOtlpExporter(opt =>
            {
                opt.Endpoint = new Uri("http://localhost:4317");
                opt.Protocol = OtlpExportProtocol.Grpc;
            })
            .Build();
        
        var tracer = tracerProvider!.GetTracer("Service F");

        using var parentSpan = tracer.StartActiveSpan("parent span A");
        parentSpan.SetAttribute("priority", "business.priority");
        parentSpan.SetAttribute("prodEnv", true);
                
        var childSpan = tracer.StartActiveSpan("child span");
        childSpan.AddEvent("Authentication").SetAttribute("Username",
            "value").SetAttribute("Id", 101);
        childSpan.SetStatus(OpenTelemetry.Trace.Status.Ok);
                
        var childChildSpan = tracer.StartActiveSpan("child child span");

        for (var i = 0; i < 5; i++)
        {
            tracer.StartActiveSpan(i.ToString()).End();
        }
                
        childChildSpan.End();
        childSpan.End();
    }
    
}