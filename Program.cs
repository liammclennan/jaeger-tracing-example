using System.Diagnostics;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace GettingStarted;

public class Program
{
    /// <summary>
    /// Sends OTEL tracing to Jaeger via gRPC. Start with:
    ///
    /// docker run -d --name jaeger \
    /// -e COLLECTOR_OTLP_ENABLED=true \
    /// -p 16686:16686 \
    /// -p 4317:4317 \
    /// -p 4318:4318 \
    /// jaegertracing/all-in-one:latest
    /// </summary>
    public static void Main()
    {
        var tracer = new TraceThing();
        tracer.Go();

        Console.ReadLine();
    }
}