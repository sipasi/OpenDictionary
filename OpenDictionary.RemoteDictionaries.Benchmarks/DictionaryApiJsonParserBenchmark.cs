#pragma warning disable

using BenchmarkDotNet.Attributes;

namespace OpenDictionary.RemoteDictionaries.Benchmarks;

[MemoryDiagnoser]
public class DictionaryApiJsonParserBenchmark
{
    [Params(1, 50, 100)]
    public int times;

    private string insistSingle;
    private string insistArray;

    private Parsers.Nodes.DictionaryApiJsonParser nodesParser;
    private Parsers.Documents.DictionaryApiJsonParser documentsParser;
    private Parsers.Newtonsoft.DictionaryApiJsonParser newtonsoftParser;


    [GlobalSetup]
    public void Setup()
    {
        nodesParser = new();
        documentsParser = new();
        newtonsoftParser = new();

        insistSingle = File.ReadAllText("insist-single.json");
        insistArray = File.ReadAllText("insist-array.json");
    }

    [Benchmark]
    public void Documents_Array()
    {
        for (int i = 0; i < times; i++)
        {
            documentsParser.Parse(insistArray);
        }
    }
    [Benchmark]
    public void Documents_Single()
    {
        for (int i = 0; i < times; i++)
        {
            documentsParser.Parse(insistSingle);
        }
    }

    [Benchmark]
    public void Nodes_Array()
    {
        for (int i = 0; i < times; i++)
        {
            nodesParser.Parse(insistArray);
        }
    }
    [Benchmark]
    public void Nodes_Single()
    {
        for (int i = 0; i < times; i++)
        {
            nodesParser.Parse(insistSingle);
        }
    }

    [Benchmark]
    public void Newtonsoft_Array()
    {
        for (int i = 0; i < times; i++)
        {
            newtonsoftParser.Parse(insistArray);
        }
    }
    [Benchmark]
    public void Newtonsoft_Single()
    {
        for (int i = 0; i < times; i++)
        {
            newtonsoftParser.Parse(insistSingle);
        }
    }
}