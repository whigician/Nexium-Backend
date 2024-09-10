using Nexium.API.Entities;

namespace Nexium.UnitTests.Fixtures;

public static class IndustriesFixture
{
    public static List<Industry> GetTestIndustries()
    {
        return
        [
            new Industry
            {
                Id = 1,
                Label = "Software"
            },

            new Industry
            {
                Id = 2,
                Label = "Manufacturing"
            },

            new Industry
            {
                Id = 3,
                Label = "Other"
            }
        ];
    }
}