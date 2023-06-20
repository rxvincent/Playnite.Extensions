using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Moq.Contrib.HttpClient;
using TestUtils;
using Xunit;
using Xunit.Abstractions;

namespace DLSiteMetadata.Test;

public class DLSiteTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public DLSiteTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Theory]
    [InlineData("RJ246037-EN.html", "https://www.dlsite.com/maniax/work/=/product_id/RJ246037.html/?locale=zh_CN")]
    [InlineData("RJ246037-JP.html", "https://www.dlsite.com/maniax/work/=/product_id/RJ246037.html/?locale=ja_JP")]
    public async Task TestScrapper(string file, string id)
    {
        var scrapper = new Scrapper(new XunitLogger<Scrapper>(_testOutputHelper), new HttpClientHandler());
        var res = await scrapper.ScrapGamePage(id);

        Assert.NotNull(res.Title);
        Assert.NotNull(res.Categories);
        Assert.NotEmpty(res.Categories!);
        Assert.NotNull(res.Genres);
        Assert.NotEmpty(res.Genres!);
        Assert.NotNull(res.Maker);
        Assert.NotNull(res.ProductImages);
        Assert.NotEmpty(res.ProductImages!);
        Assert.NotNull(res.Illustrators);
        Assert.NotEmpty(res.Illustrators!);
        Assert.NotNull(res.MusicCreators);
        Assert.NotEmpty(res.MusicCreators!);
        Assert.NotNull(res.ScenarioWriters);
        Assert.NotEmpty(res.ScenarioWriters!);
        Assert.NotNull(res.VoiceActors);
        Assert.NotEmpty(res.VoiceActors!);
        Assert.NotNull(res.Icon);
    }

    [Theory]
    [InlineData("search-EN.html", "ONEONE1", "en_US")]
    [InlineData("search-JP.html", "ONEONE1", "ja_JP")]
    public async Task TestScrapSearchPage(string file, string term, string language)
    {
        var scrapper = new Scrapper(new XunitLogger<Scrapper>(_testOutputHelper), new HttpClientHandler());
        var results = await scrapper.ScrapSearchPage(term, default, 100, language);
        Assert.NotEmpty(results);
    }
}
