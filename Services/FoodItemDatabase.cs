using CraftingCalc.Models;

namespace CraftingCalc.Services;

public static class FoodItemDatabase
{
    private static FoodIngredient I(string apiId, string name, int qty) => new(apiId, name, qty);

    // Sauce count = qty per crafting batch (matches @count in game data enchantment craftresource)
    private static readonly Dictionary<string, int> EnchantSauceQty = new()
    {
        // ── Batch = 10 ───────────────────────────────────────────────────────
        { "T1_MEAL_SOUP",              10 }, { "T2_MEAL_SALAD",             10 },
        { "T3_MEAL_PIE",               10 }, { "T3_MEAL_OMELETTE",          10 },
        { "T3_MEAL_OMELETTE_AVALON",   10 }, { "T3_MEAL_ROAST",             10 },
        { "T4_MEAL_STEW",              10 }, { "T4_MEAL_STEW_AVALON",       10 },
        { "T4_MEAL_SANDWICH",          10 }, { "T4_MEAL_SANDWICH_AVALON",   10 },

        { "T3_MEAL_SOUP",              30 }, { "T4_MEAL_SALAD",             30 },
        { "T5_MEAL_PIE",               30 }, { "T5_MEAL_OMELETTE",          30 },
        { "T5_MEAL_OMELETTE_AVALON",   30 }, { "T5_MEAL_ROAST",             30 },
        { "T6_MEAL_STEW",              30 }, { "T6_MEAL_STEW_AVALON",       30 },
        { "T6_MEAL_SANDWICH",          30 }, { "T6_MEAL_SANDWICH_AVALON",   30 },

        { "T5_MEAL_SOUP",              90 }, { "T6_MEAL_SALAD",             90 },
        { "T7_MEAL_PIE",               90 }, { "T7_MEAL_OMELETTE",          90 },
        { "T7_MEAL_OMELETTE_AVALON",   90 }, { "T7_MEAL_ROAST",             90 },
        { "T8_MEAL_STEW",              90 }, { "T8_MEAL_STEW_AVALON",       90 },
        { "T8_MEAL_SANDWICH",          90 }, { "T8_MEAL_SANDWICH_AVALON",   90 },

        // ── Batch = 1 (fish meals) ────────────────────────────────────────
        { "T1_MEAL_SOUP_FISH",          3 }, { "T2_MEAL_SALAD_FISH",         3 },
        { "T3_MEAL_PIE_FISH",           3 }, { "T3_MEAL_OMELETTE_FISH",      3 },
        { "T3_MEAL_ROAST_FISH",         3 }, { "T4_MEAL_STEW_FISH",          3 },
        { "T4_MEAL_SANDWICH_FISH",      3 },

        { "T3_MEAL_SOUP_FISH",          9 }, { "T4_MEAL_SALAD_FISH",         9 },
        { "T5_MEAL_PIE_FISH",           9 }, { "T5_MEAL_OMELETTE_FISH",      9 },
        { "T5_MEAL_ROAST_FISH",         9 }, { "T6_MEAL_STEW_FISH",          9 },
        { "T6_MEAL_SANDWICH_FISH",      9 },

        { "T5_MEAL_SOUP_FISH",         27 }, { "T6_MEAL_SALAD_FISH",        27 },
        { "T7_MEAL_PIE_FISH",          27 }, { "T7_MEAL_OMELETTE_FISH",     27 },
        { "T7_MEAL_ROAST_FISH",        27 }, { "T8_MEAL_STEW_FISH",         27 },
        { "T8_MEAL_SANDWICH_FISH",     27 },
    };

    private static IEnumerable<FoodItem> Enchanted(FoodItem base_) =>
        Enumerable.Range(1, 3).Select(lvl => base_ with
        {
            ApiId            = $"{base_.ApiId}@{lvl}",
            Name             = $"{base_.Name} .{lvl}",
            EnchantmentLevel = lvl,
            RequiredSauce    = I($"T1_FISHSAUCE_LEVEL{lvl}", $"Fish Sauce (Level {lvl})", EnchantSauceQty[base_.ApiId]),
        });

    public static readonly IReadOnlyList<FoodItem> Items;

    static FoodItemDatabase()
    {
        var base_ = new List<FoodItem>
        {
        // ── T1 ──────────────────────────────────────────────────────────────
        new("T1_MEAL_SOUP",         "Carrot Soup",              1, 10,
            I("T1_CARROT",  "Carrots",     16), null, null, null,
            "Fort Sterling"),

        new("T1_MEAL_SEAWEEDSALAD", "Seaweed Salad",            1,  1,
            I("T1_SEAWEED", "Seaweed",     10), null, null, null,
            "Fort Sterling"),

        new("T1_MEAL_GRILLEDFISH",  "Grilled Fish",             1,  1,
            I("T1_FISHCHOPS","Chopped Fish",10), null, null, null,
            "Thetford"),

        new("T1_MEAL_SOUP_FISH",    "Greenmoor Clam Soup",      1,  1,
            I("T3_FISH_FRESHWATER_SWAMP_RARE","Greenmoor Clam",   1),
            I("T1_CARROT",                    "Carrots",           2),
            null, null,
            "Fort Sterling"),

        // ── T2 ──────────────────────────────────────────────────────────────
        new("T2_MEAL_SALAD",        "Bean Salad",               2, 10,
            I("T2_BEAN",    "Beans",        8),
            I("T1_CARROT",  "Carrots",      8),
            null, null,
            "Fort Sterling"),

        new("T2_MEAL_SALAD_FISH",   "Shallowshore Squid Salad", 2,  1,
            I("T3_FISH_SALTWATER_ALL_RARE","Shallowshore Squid",  1),
            I("T2_BEAN",                  "Beans",                1),
            I("T2_AGARIC",                "Arcane Agaric",        1),
            null,
            "Brecilien"),

        // ── T3 ──────────────────────────────────────────────────────────────
        new("T3_MEAL_OMELETTE",     "Chicken Omelette",         3, 10,
            I("T3_WHEAT",   "Sheaf of Wheat",  4),
            I("T3_MEAT",    "Raw Chicken",      8),
            I("T3_EGG",     "Hen Eggs",         2),
            null,
            "Lymhurst"),

        new("T3_MEAL_PIE",          "Chicken Pie",              3, 10,
            I("T3_WHEAT",   "Sheaf of Wheat",  2),
            I("T3_FLOUR",   "Flour",            4),
            I("T3_MEAT",    "Raw Chicken",      8),
            null,
            "Lymhurst"),

        new("T3_MEAL_ROAST",        "Roast Chicken",            3, 10,
            I("T3_MEAT",    "Raw Chicken",      8),
            I("T2_BEAN",    "Beans",            4),
            I("T4_MILK",    "Goat's Milk",      4),
            null,
            "Lymhurst"),

        new("T3_MEAL_SOUP",         "Wheat Soup",               3, 10,
            I("T3_WHEAT",   "Sheaf of Wheat",  48), null, null, null,
            "Fort Sterling"),

        new("T3_MEAL_OMELETTE_AVALON", "Avalonian Chicken Omelette", 3, 10,
            I("T4_MILK",                  "Goat's Milk",          4),
            I("T3_MEAT",                  "Raw Chicken",          8),
            I("T3_EGG",                   "Hen Eggs",             2),
            I("QUESTITEM_TOKEN_AVALON",   "Avalonian Energy",    10),
            "Lymhurst"),

        new("T3_MEAL_OMELETTE_FISH","Lowriver Crab Omelette",   3,  1,
            I("T3_FISH_FRESHWATER_STEPPE_RARE","Lowriver Crab",    1),
            I("T3_COMFREY",                    "Brightleaf Comfrey",1),
            I("T3_EGG",                        "Hen Eggs",           1),
            null,
            "Bridgewatch"),

        new("T3_MEAL_PIE_FISH",     "Upland Coldeye Pie",       3,  1,
            I("T3_FISH_FRESHWATER_MOUNTAIN_RARE","Upland Coldeye",  1),
            I("T3_FLOUR",                        "Flour",            1),
            I("T3_EGG",                          "Hen Eggs",         1),
            null,
            "Lymhurst"),

        new("T3_MEAL_ROAST_FISH",   "Roasted Whitefog Snapper", 3,  1,
            I("T3_FISH_FRESHWATER_AVALON_RARE","Whitefog Snapper",  1),
            I("T3_COMFREY",                   "Brightleaf Comfrey", 1),
            I("T4_MILK",                      "Goat's Milk",        1),
            null,
            "Brecilien"),

        new("T3_MEAL_SOUP_FISH",    "Murkwater Clam Soup",      3,  1,
            I("T5_FISH_FRESHWATER_SWAMP_RARE","Murkwater Clam",     1),
            I("T3_WHEAT",                    "Sheaf of Wheat",       2),
            I("T3_COMFREY",                  "Brightleaf Comfrey",   2),
            I("T3_MEAT",                     "Raw Chicken",          2),
            "Bridgewatch"),

        // ── T4 ──────────────────────────────────────────────────────────────
        new("T4_MEAL_SALAD",        "Turnip Salad",             4, 10,
            I("T4_TURNIP",  "Turnips",         24),
            I("T3_WHEAT",   "Sheaf of Wheat",  24),
            null, null,
            "Fort Sterling"),

        new("T4_MEAL_SANDWICH",     "Goat Sandwich",            4, 10,
            I("T4_BREAD",   "Bread",            4),
            I("T4_MEAT",    "Raw Goat",         8),
            I("T4_BUTTER",  "Goat's Butter",    2),
            null,
            "Fort Sterling"),

        new("T4_MEAL_STEW",         "Goat Stew",                4, 10,
            I("T4_TURNIP",  "Turnips",          4),
            I("T4_BREAD",   "Bread",            4),
            I("T4_MEAT",    "Raw Goat",         8),
            null,
            "Fort Sterling"),

        new("T4_MEAL_SANDWICH_AVALON","Avalonian Goat Sandwich", 4, 10,
            I("T4_BREAD",                 "Bread",              4),
            I("T4_MEAT",                  "Raw Goat",           8),
            I("T4_BUTTER",                "Goat's Butter",      2),
            I("QUESTITEM_TOKEN_AVALON",   "Avalonian Energy",  10),
            "Fort Sterling"),

        new("T4_MEAL_STEW_AVALON",  "Avalonian Goat Stew",      4, 10,
            I("T1_CARROT",                "Carrots",            4),
            I("T4_TURNIP",                "Turnips",            4),
            I("T4_MEAT",                  "Raw Goat",           8),
            I("QUESTITEM_TOKEN_AVALON",   "Avalonian Energy",  10),
            "Fort Sterling"),

        new("T4_MEAL_SALAD_FISH",   "Midwater Octopus Salad",   4,  1,
            I("T5_FISH_SALTWATER_ALL_RARE","Midwater Octopus",   1),
            I("T4_TURNIP",                "Turnips",             2),
            I("T4_BURDOCK",               "Crenellated Burdock", 2),
            I("T4_MEAT",                  "Raw Goat",            2),
            "Brecilien"),

        new("T4_MEAL_SANDWICH_FISH","Stonestream Lurcher Sandwich",4,1,
            I("T3_FISH_FRESHWATER_HIGHLANDS_RARE","Stonestream Lurcher",1),
            I("T4_TURNIP",                        "Turnips",             1),
            I("T4_BUTTER",                        "Goat's Butter",       1),
            null,
            "Bridgewatch"),

        new("T4_MEAL_STEW_FISH",    "Greenriver Eel Stew",      4,  1,
            I("T3_FISH_FRESHWATER_FOREST_RARE","Greenriver Eel",    1),
            I("T4_TURNIP",                    "Turnips",             1),
            I("T4_BURDOCK",                   "Crenellated Burdock", 1),
            null,
            "Lymhurst"),

        // ── T5 ──────────────────────────────────────────────────────────────
        new("T5_MEAL_OMELETTE",     "Goose Omelette",           5, 10,
            I("T5_CABBAGE", "Cabbage",         12),
            I("T5_MEAT",    "Raw Goose",       24),
            I("T5_EGG",     "Goose Eggs",       6),
            null,
            "Martlock"),

        new("T5_MEAL_PIE",          "Goose Pie",                5, 10,
            I("T5_CABBAGE", "Cabbage",          6),
            I("T3_FLOUR",   "Flour",           12),
            I("T5_MEAT",    "Raw Goose",       24),
            I("T4_MILK",    "Goat's Milk",      6),
            "Martlock"),

        new("T5_MEAL_ROAST",        "Roast Goose",              5, 10,
            I("T5_MEAT",    "Raw Goose",       24),
            I("T5_CABBAGE", "Cabbage",         12),
            I("T6_MILK",    "Sheep's Milk",    12),
            null,
            "Martlock"),

        new("T5_MEAL_SOUP",         "Cabbage Soup",             5, 10,
            I("T5_CABBAGE", "Cabbage",        144), null, null, null,
            "Lymhurst"),

        new("T5_MEAL_OMELETTE_AVALON","Avalonian Goose Omelette",5, 10,
            I("T6_MILK",                  "Sheep's Milk",      12),
            I("T5_MEAT",                  "Raw Goose",         24),
            I("T5_EGG",                   "Goose Eggs",         6),
            I("QUESTITEM_TOKEN_AVALON",   "Avalonian Energy",  30),
            "Martlock"),

        new("T5_MEAL_OMELETTE_FISH","Drybrook Crab Omelette",   5,  1,
            I("T5_FISH_FRESHWATER_STEPPE_RARE","Drybrook Crab",  1),
            I("T5_CABBAGE",                   "Cabbage",          2),
            I("T5_TEASEL",                    "Dragon Teasel",    2),
            I("T5_EGG",                       "Goose Eggs",       2),
            "Martlock"),

        new("T5_MEAL_PIE_FISH",     "Mountain Blindeye Pie",    5,  1,
            I("T5_FISH_FRESHWATER_MOUNTAIN_RARE","Mountain Blindeye",1),
            I("T5_CABBAGE",                     "Cabbage",           2),
            I("T5_TEASEL",                      "Dragon Teasel",     2),
            I("T5_EGG",                         "Goose Eggs",        2),
            "Martlock"),

        new("T5_MEAL_ROAST_FISH",   "Roasted Clearhaze Snapper",5,  1,
            I("T5_FISH_FRESHWATER_AVALON_RARE","Clearhaze Snapper",  1),
            I("T5_CABBAGE",                   "Cabbage",              2),
            I("T5_TEASEL",                    "Dragon Teasel",        2),
            I("T6_MILK",                      "Sheep's Milk",         2),
            "Brecilien"),

        new("T5_MEAL_SOUP_FISH",    "Blackbog Clam Soup",       5,  1,
            I("T7_FISH_FRESHWATER_SWAMP_RARE","Blackbog Clam",  1),
            I("T5_CABBAGE",                  "Cabbage",          6),
            I("T5_TEASEL",                   "Dragon Teasel",    6),
            I("T5_MEAT",                     "Raw Goose",        6),
            "Brecilien"),

        // ── T6 ──────────────────────────────────────────────────────────────
        new("T6_MEAL_SALAD",        "Potato Salad",             6, 10,
            I("T6_POTATO",  "Potatoes",        72),
            I("T5_CABBAGE", "Cabbage",         72),
            null, null,
            "Bridgewatch"),

        new("T6_MEAL_SANDWICH",     "Mutton Sandwich",          6, 10,
            I("T4_BREAD",   "Bread",           12),
            I("T6_MEAT",    "Raw Mutton",      24),
            I("T6_BUTTER",  "Sheep's Butter",   6),
            null,
            "Bridgewatch"),

        new("T6_MEAL_STEW",         "Mutton Stew",              6, 10,
            I("T6_POTATO",  "Potatoes",        12),
            I("T4_BREAD",   "Bread",           12),
            I("T6_MEAT",    "Raw Mutton",      24),
            null,
            "Bridgewatch"),

        new("T6_MEAL_SANDWICH_AVALON","Avalonian Mutton Sandwich",6,10,
            I("T4_BREAD",                 "Bread",             12),
            I("T6_MEAT",                  "Raw Mutton",        24),
            I("T6_BUTTER",                "Sheep's Butter",     6),
            I("QUESTITEM_TOKEN_AVALON",   "Avalonian Energy",  30),
            "Bridgewatch"),

        new("T6_MEAL_STEW_AVALON",  "Avalonian Mutton Stew",    6, 10,
            I("T5_CABBAGE",               "Cabbage",           12),
            I("T6_POTATO",                "Potatoes",          12),
            I("T6_MEAT",                  "Raw Mutton",        24),
            I("QUESTITEM_TOKEN_AVALON",   "Avalonian Energy",  30),
            "Bridgewatch"),

        new("T6_MEAL_SALAD_FISH",   "Deepwater Kraken Salad",   6,  1,
            I("T7_FISH_SALTWATER_ALL_RARE","Deepwater Kraken",  1),
            I("T6_POTATO",               "Potatoes",            6),
            I("T6_FOXGLOVE",             "Elusive Foxglove",    6),
            I("T6_MEAT",                 "Raw Mutton",          6),
            "Brecilien"),

        new("T6_MEAL_SANDWICH_FISH","Rushwater Lurcher Sandwich",6,  1,
            I("T5_FISH_FRESHWATER_HIGHLANDS_RARE","Rushwater Lurcher",1),
            I("T6_POTATO",                       "Potatoes",          2),
            I("T6_FOXGLOVE",                     "Elusive Foxglove",  2),
            I("T6_BUTTER",                       "Sheep's Butter",    2),
            "Bridgewatch"),

        new("T6_MEAL_STEW_FISH",    "Redspring Eel Stew",       6,  1,
            I("T5_FISH_FRESHWATER_FOREST_RARE","Redspring Eel",  1),
            I("T6_POTATO",                    "Potatoes",         2),
            I("T6_FOXGLOVE",                  "Elusive Foxglove", 2),
            I("T6_MILK",                      "Sheep's Milk",     2),
            "Brecilien"),

        // ── T7 ──────────────────────────────────────────────────────────────
        new("T7_MEAL_OMELETTE",     "Pork Omelette",            7, 10,
            I("T7_CORN",    "Bundle of Corn",  36),
            I("T7_MEAT",    "Raw Pork",        72),
            I("T5_EGG",     "Goose Eggs",      18),
            null,
            "Martlock"),

        new("T7_MEAL_PIE",          "Pork Pie",                 7, 10,
            I("T7_CORN",    "Bundle of Corn",  18),
            I("T3_FLOUR",   "Flour",           36),
            I("T7_MEAT",    "Raw Pork",        72),
            I("T6_MILK",    "Sheep's Milk",    18),
            "Martlock"),

        new("T7_MEAL_ROAST",        "Roast Pork",               7, 10,
            I("T7_MEAT",    "Raw Pork",        72),
            I("T7_CORN",    "Bundle of Corn",  36),
            I("T8_MILK",    "Cow's Milk",      36),
            null,
            "Martlock"),

        new("T7_MEAL_OMELETTE_AVALON","Avalonian Pork Omelette", 7, 10,
            I("T8_MILK",                  "Cow's Milk",        36),
            I("T7_MEAT",                  "Raw Pork",          72),
            I("T5_EGG",                   "Goose Eggs",        18),
            I("QUESTITEM_TOKEN_AVALON",   "Avalonian Energy",  90),
            "Martlock"),

        new("T7_MEAL_OMELETTE_FISH","Dusthole Crab Omelette",   7,  1,
            I("T7_FISH_FRESHWATER_STEPPE_RARE","Dusthole Crab",  1),
            I("T7_CORN",                      "Bundle of Corn",  6),
            I("T7_MULLEIN",                   "Firetouched Mullein",6),
            I("T7_MEAT",                      "Raw Pork",        6),
            "Martlock"),

        new("T7_MEAL_PIE_FISH",     "Frostpeak Deadeye Pie",    7,  1,
            I("T7_FISH_FRESHWATER_MOUNTAIN_RARE","Frostpeak Deadeye",1),
            I("T7_CORN",                        "Bundle of Corn",   6),
            I("T7_MULLEIN",                     "Firetouched Mullein",6),
            I("T7_MEAT",                        "Raw Pork",         6),
            "Martlock"),

        new("T7_MEAL_ROAST_FISH",   "Roasted Puremist Snapper", 7,  1,
            I("T7_FISH_FRESHWATER_AVALON_RARE","Puremist Snapper",  1),
            I("T7_CORN",                      "Bundle of Corn",     6),
            I("T7_MULLEIN",                   "Firetouched Mullein",6),
            I("T8_MILK",                      "Cow's Milk",         6),
            "Brecilien"),

        // ── T8 ──────────────────────────────────────────────────────────────
        new("T8_MEAL_SANDWICH",     "Beef Sandwich",            8, 10,
            I("T4_BREAD",   "Bread",           36),
            I("T8_MEAT",    "Raw Beef",        72),
            I("T8_BUTTER",  "Cow's Butter",    18),
            null,
            "Bridgewatch"),

        new("T8_MEAL_STEW",         "Beef Stew",                8, 10,
            I("T8_PUMPKIN", "Pumpkin",         36),
            I("T4_BREAD",   "Bread",           36),
            I("T8_MEAT",    "Raw Beef",        72),
            null,
            "Bridgewatch"),

        new("T8_MEAL_SANDWICH_AVALON","Avalonian Beef Sandwich", 8, 10,
            I("T4_BREAD",                 "Bread",             36),
            I("T8_MEAT",                  "Raw Beef",          72),
            I("T8_BUTTER",                "Cow's Butter",      18),
            I("QUESTITEM_TOKEN_AVALON",   "Avalonian Energy",  90),
            "Bridgewatch"),

        new("T8_MEAL_STEW_AVALON",  "Avalonian Beef Stew",      8, 10,
            I("T7_CORN",                  "Bundle of Corn",    36),
            I("T8_PUMPKIN",               "Pumpkin",           36),
            I("T8_MEAT",                  "Raw Beef",          72),
            I("QUESTITEM_TOKEN_AVALON",   "Avalonian Energy",  90),
            "Bridgewatch"),

        new("T8_MEAL_SANDWICH_FISH","Thunderfall Lurcher Sandwich",8,1,
            I("T7_FISH_FRESHWATER_HIGHLANDS_RARE","Thunderfall Lurcher",1),
            I("T8_PUMPKIN",                      "Pumpkin",             6),
            I("T8_YARROW",                       "Ghoul Yarrow",        6),
            I("T8_BUTTER",                       "Cow's Butter",        6),
            "Brecilien"),

        new("T8_MEAL_STEW_FISH",    "Deadwater Eel Stew",       8,  1,
            I("T7_FISH_FRESHWATER_FOREST_RARE","Deadwater Eel",  1),
            I("T8_PUMPKIN",                  "Pumpkin",           6),
            I("T8_YARROW",                   "Ghoul Yarrow",      6),
            I("T8_MILK",                     "Cow's Milk",        6),
            "Brecilien"),
        };

        Items = base_
            .Concat(base_.Where(i => EnchantSauceQty.ContainsKey(i.ApiId)).SelectMany(Enchanted))
            .ToList();
    }

    public static bool IsEnchantable(string baseApiId) => EnchantSauceQty.ContainsKey(baseApiId);

    public static FoodItem? GetVariant(string baseApiId, int enchantmentLevel) =>
        Items.FirstOrDefault(i => i.ApiId == (enchantmentLevel == 0 ? baseApiId : $"{baseApiId}@{enchantmentLevel}"));

    public static string GetPreferredCityForMat(string id)
    {
        if (id.Contains("T8_MEAT") || id.Contains("T6_MEAT") || id.Contains("T8_BUTTER") || id.Contains("T6_BUTTER") || id.Contains("T8_PUMPKIN") || id.Contains("T6_POTATO")) return "Bridgewatch";
        if (id.Contains("T7_MEAT") || id.Contains("_FRESHWATER_SWAMP_")) return "Thetford";
        if (id.Contains("T5_MEAT") || id.Contains("T5_EGG") || id.Contains("T5_CABBAGE") || id.Contains("T7_CORN") || id.Contains("T7_MULLEIN") || id.Contains("_FRESHWATER_MOUNTAIN_")) return "Martlock";
        if (id.Contains("T3_MEAT") || id.Contains("T3_EGG") || id.Contains("_FRESHWATER_FOREST_")) return "Lymhurst";
        if (id.Contains("_SALTWATER_") || id.Contains("_FRESHWATER_AVALON_") || id == "QUESTITEM_TOKEN_AVALON" || id.Contains("T1_FISHCHOPS") || id.Contains("T1_SEAWEED")) return "Brecilien";
        if (id.Contains("_FRESHWATER_STEPPE_") || id.Contains("_FRESHWATER_HIGHLANDS_")) return "Bridgewatch";
        return "Fort Sterling";
    }
}