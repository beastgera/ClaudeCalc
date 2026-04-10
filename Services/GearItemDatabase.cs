using CraftingCalc.Models;

namespace CraftingCalc.Services;

public static class GearItemDatabase
{
    // Material type names used in Albion API
    public const string Planks = "PLANKS";
    public const string Metalbar = "METALBAR";
    public const string Leather = "LEATHER";
    public const string Cloth = "CLOTH";
    public const string Stoneblock = "STONEBLOCK";

    // Journal types
    public const int Mage = 1;
    public const int Hunter = 2;
    public const int Warrior = 3;
    public const int Royal = 4;

    public static readonly IReadOnlyList<GearItem> Items = new List<GearItem>
    {
        new("MAIN_ARCANESTAFF", "Arcane Staff", 16, Planks, 8, Metalbar, "Lymhurst", null, null, 0, null, null, 0, Mage, 250),
        new("ARMOR_PLATE_AVALON", "Armor of Valor", 16, Metalbar, 0, null, "Bridgewatch", "ARTEFACT_ARMOR_PLATE_AVALON", "Exalted Plating", 1, null, null, 0, Warrior, 250),
        new("HEAD_LEATHER_SET3", "Assassin Hood", 8, Leather, 0, null, "Lymhurst", null, null, 0, null, null, 0, Hunter, 250),
        new("ARMOR_LEATHER_SET3", "Assassin Jacket", 16, Leather, 0, null, "Thetford", null, null, 0, null, null, 0, Hunter, 250),
        new("SHOES_LEATHER_SET3", "Assassin Shoes", 8, Leather, 0, null, "Lymhurst", null, null, 0, null, null, 0, Hunter, 250),
        new("OFF_SHIELD_AVALON", "Astral Aegis", 4, Planks, 4, Metalbar, "Martlock", "ARTEFACT_OFF_SHIELD_AVALON", "Crushed Avalonian Heirloom", 1, null, null, 0, Warrior, 250),
        new("2H_TOOL_AXE_AVALON", "Avalonian Axe", 6, Planks, 2, Metalbar, "Caerleon", "QUESTITEM_TOKEN_AVALON", "Avalonian Energy", 1, null, null, 0, Royal, 250),
        new("2H_TOOL_SIEGEHAMMER_AVALON", "Avalonian Demolition Hammer", 8, Planks, 8, Stoneblock, "Caerleon", "QUESTITEM_TOKEN_AVALON", "Avalonian Energy", 1, null, null, 0, Royal, 370),
        new("2H_TOOL_FISHINGROD_AVALON", "Avalonian Fishing Rod", 6, Planks, 2, Cloth, "Caerleon", "QUESTITEM_TOKEN_AVALON", "Avalonian Energy", 1, null, null, 0, Royal, 250),
        new("2H_TOOL_PICK_AVALON", "Avalonian Pickaxe", 6, Planks, 2, Metalbar, "Caerleon", "QUESTITEM_TOKEN_AVALON", "Avalonian Energy", 1, null, null, 0, Royal, 250),
        new("2H_TOOL_SICKLE_AVALON", "Avalonian Sickle", 6, Planks, 2, Metalbar, "Caerleon", "QUESTITEM_TOKEN_AVALON", "Avalonian Energy", 1, null, null, 0, Royal, 250),
        new("2H_TOOL_KNIFE_AVALON", "Avalonian Skinning Knife", 6, Planks, 2, Metalbar, "Caerleon", "QUESTITEM_TOKEN_AVALON", "Avalonian Energy", 1, null, null, 0, Royal, 250),
        new("2H_TOOL_HAMMER_AVALON", "Avalonian Stone Hammer", 6, Planks, 2, Metalbar, "Caerleon", "QUESTITEM_TOKEN_AVALON", "Avalonian Energy", 1, null, null, 0, Royal, 250),
        new("2H_TOOL_AXE", "Axe", 6, Planks, 2, Metalbar, "Caerleon", null, null, 0, null, null, 0, Royal, 250),
        new("BAG", "Bag", 8, Cloth, 8, Leather, "Brecilien", null, null, 0, null, null, 0, Royal, 340),
        new("2H_KNUCKLES_SET2", "Battle Bracers", 20, Leather, 12, Metalbar, "Caerleon", null, null, 0, null, null, 0, Warrior, 250),
        new("MAIN_AXE", "Battleaxe", 8, Planks, 16, Metalbar, "Martlock", null, null, 0, null, null, 0, Warrior, 250),
        new("2H_DUALAXE_KEEPER", "Bear Paws", 12, Planks, 20, Metalbar, "Martlock", "ARTEFACT_2H_DUALAXE_KEEPER", "Keeper Axeheads", 1, null, null, 0, Warrior, 250),
        new("MAIN_ROCKMACE_KEEPER", "Bedrock Mace", 16, Metalbar, 8, Cloth, "Thetford", "ARTEFACT_MAIN_ROCKMACE_KEEPER", "Runed Rock", 1, null, null, 0, Warrior, 250),
        new("2H_COMBATSTAFF_MORGANA", "Black Monk Stave", 12, Metalbar, 20, Leather, "Martlock", "ARTEFACT_2H_COMBATSTAFF_MORGANA", "Reinforced Morgana Pole", 1, null, null, 0, Hunter, 250),
        new("2H_INFERNOSTAFF_MORGANA", "Blazing Staff", 20, Planks, 12, Metalbar, "Thetford", "ARTEFACT_2H_INFERNOSTAFF_MORGANA", "Unholy Scroll", 1, null, null, 0, Mage, 250),
        new("2H_NATURESTAFF_HELL", "Blight Staff", 20, Planks, 12, Cloth, "Thetford", "ARTEFACT_2H_NATURESTAFF_HELL", "Symbol Of Blight", 1, null, null, 0, Hunter, 250),
        new("MAIN_RAPIER_MORGANA", "Bloodletter", 16, Metalbar, 8, Leather, "Bridgewatch", "ARTEFACT_MAIN_RAPIER_MORGANA", "Hardened Debole", 1, null, null, 0, Hunter, 250),
        new("2H_SHAPESHIFTER_MORGANA", "Bloodmoon Staff", 20, Planks, 12, Leather, "Caerleon", "ARTEFACT_2H_SHAPESHIFTER_MORGANA", "Werewolf Remnant", 1, "ALCHEMY_RARE_WEREWOLF", "Werewolf Fangs", 1, Hunter, 250),
        new("2H_DUALCROSSBOW_HELL", "Boltcasters", 20, Planks, 12, Metalbar, "Bridgewatch", "ARTEFACT_2H_DUALCROSSBOW_HELL", "Hellish Bolts", 1, null, null, 0, Warrior, 250),
        new("SHOES_PLATE_AVALON", "Boots of Valor", 8, Metalbar, 0, null, "Martlock", "ARTEFACT_SHOES_PLATE_AVALON", "Exalted Greave", 1, null, null, 0, Warrior, 250),
        new("2H_BOW", "Bow", 32, Planks, 0, null, "Lymhurst", null, null, 0, null, null, 0, Hunter, 250),
        new("2H_BOW_KEEPER", "Bow of Badon", 32, Planks, 0, null, "Lymhurst", "ARTEFACT_2H_BOW_KEEPER", "Carved Bone", 1, null, null, 0, Hunter, 250),
        new("2H_KNUCKLES_SET1", "Brawler Gloves", 20, Leather, 12, Metalbar, "Caerleon", null, null, 0, null, null, 0, Warrior, 250),
        new("2H_DAGGER_KATAR_AVALON", "Bridled Fury", 20, Leather, 12, Metalbar, "Bridgewatch", "ARTEFACT_2H_DAGGER_KATAR_AVALON", "Bloodstained Antiquities", 1, null, null, 0, Hunter, 250),
        new("2H_FIRESTAFF_HELL", "Brimstone Staff", 20, Planks, 12, Metalbar, "Thetford", "ARTEFACT_2H_FIRESTAFF_HELL", "Burning Orb", 1, null, null, 0, Mage, 250),
        new("MAIN_SWORD", "Broadsword", 16, Metalbar, 8, Leather, "Lymhurst", null, null, 0, null, null, 0, Warrior, 250),
        new("OFF_SHIELD_HELL", "Caitiff Shield", 4, Planks, 4, Metalbar, "Martlock", "ARTEFACT_OFF_SHIELD_HELL", "Infernal Shield Core", 1, null, null, 0, Warrior, 250),
        new("2H_MACE_MORGANA", "Camlann Mace", 20, Metalbar, 12, Cloth, "Thetford", "ARTEFACT_2H_MACE_MORGANA", "Imbued Mace Head", 1, null, null, 0, Warrior, 250),
        new("CAPE", "Cape", 4, Cloth, 4, Leather, "Brecilien", null, null, 0, null, null, 0, Royal, 370),
        new("2H_HALBERD_MORGANA", "Carrioncaller", 20, Planks, 12, Metalbar, "Martlock", "ARTEFACT_2H_HALBERD_MORGANA", "Morgana Halberd Head", 1, null, null, 0, Warrior, 250),
        new("2H_CLEAVER_HELL", "Carving Sword", 20, Metalbar, 12, Leather, "Lymhurst", "ARTEFACT_2H_CLEAVER_HELL", "Demonic Blade", 1, null, null, 0, Warrior, 250),
        new("OFF_CENSER_AVALON", "Celestial Censer", 4, Cloth, 4, Leather, "Martlock", "ARTEFACT_OFF_CENSER_AVALON", "Severed Celestial Keepsake", 1, null, null, 0, Mage, 250),
        new("MAIN_FROSTSTAFF_AVALON", "Chillhowl", 16, Planks, 8, Metalbar, "Martlock", "ARTEFACT_MAIN_FROSTSTAFF_AVALON", "Chilled Crystaline Shard", 1, null, null, 0, Mage, 250),
        new("MAIN_SCIMITAR_MORGANA", "Clarent Blade", 16, Metalbar, 8, Leather, "Lymhurst", "ARTEFACT_MAIN_SCIMITAR_MORGANA", "Bloodforged Blade", 1, null, null, 0, Warrior, 250),
        new("2H_CLAWPAIR", "Claws", 12, Metalbar, 20, Leather, "Bridgewatch", null, null, 0, null, null, 0, Hunter, 250),
        new("2H_CLAYMORE", "Claymore", 20, Metalbar, 12, Leather, "Lymhurst", null, null, 0, null, null, 0, Warrior, 250),
        new("HEAD_CLOTH_SET2", "Cleric Cowl", 8, Cloth, 0, null, "Thetford", null, null, 0, null, null, 0, Mage, 250),
        new("ARMOR_CLOTH_SET2", "Cleric Robe", 16, Cloth, 0, null, "Fort Sterling", null, null, 0, null, null, 0, Mage, 250),
        new("SHOES_CLOTH_SET2", "Cleric Sandals", 8, Cloth, 0, null, "Bridgewatch", null, null, 0, null, null, 0, Mage, 250),
        new("HEAD_CLOTH_AVALON", "Cowl of Purity", 8, Cloth, 0, null, "Thetford", "ARTEFACT_HEAD_CLOTH_AVALON", "Sanctified Mask", 1, null, null, 0, Mage, 250),
        new("2H_CROSSBOW", "Crossbow", 20, Planks, 12, Metalbar, "Bridgewatch", null, null, 0, null, null, 0, Warrior, 250),
        new("OFF_LAMP_UNDEAD", "Cryptcandle", 4, Planks, 4, Cloth, "Martlock", "ARTEFACT_OFF_LAMP_UNDEAD", "Ghastly Candle", 1, null, null, 0, Hunter, 250),
        new("HEAD_CLOTH_MORGANA", "Cultist Cowl", 8, Cloth, 0, null, "Thetford", "ARTEFACT_HEAD_CLOTH_MORGANA", "Alluring Padding", 1, null, null, 0, Mage, 250),
        new("ARMOR_CLOTH_MORGANA", "Cultist Robe", 16, Cloth, 0, null, "Fort Sterling", "ARTEFACT_ARMOR_CLOTH_MORGANA", "Alluring Amulet", 1, null, null, 0, Mage, 250),
        new("SHOES_CLOTH_MORGANA", "Cultist Sandals", 8, Cloth, 0, null, "Bridgewatch", "ARTEFACT_SHOES_CLOTH_MORGANA", "Alluring Bindings", 1, null, null, 0, Mage, 250),
        new("2H_SKULLORB_HELL", "Cursed Skull", 20, Planks, 12, Metalbar, "Bridgewatch", "ARTEFACT_2H_SKULLORB_HELL", "Cursed Jawbone", 1, null, null, 0, Mage, 250),
        new("MAIN_CURSEDSTAFF", "Cursed Staff", 16, Planks, 8, Metalbar, "Bridgewatch", null, null, 0, null, null, 0, Mage, 250),
        new("MAIN_DAGGER", "Dagger", 12, Metalbar, 12, Leather, "Bridgewatch", null, null, 0, null, null, 0, Hunter, 250),
        new("2H_DAGGERPAIR", "Dagger Pair", 16, Metalbar, 16, Leather, "Bridgewatch", null, null, 0, null, null, 0, Hunter, 250),
        new("2H_CURSEDSTAFF_MORGANA", "Damnation Staff", 20, Planks, 12, Metalbar, "Bridgewatch", "ARTEFACT_2H_CURSEDSTAFF_MORGANA", "Bloodforged Catalyst", 1, null, null, 0, Mage, 250),
        new("2H_FIRE_RINGPAIR_AVALON", "Dawnsong", 20, Planks, 12, Metalbar, "Thetford", "ARTEFACT_2H_FIRE_RINGPAIR_AVALON", "Glowing Harmonic Ring", 1, null, null, 0, Mage, 250),
        new("MAIN_SPEAR_LANCE_AVALON", "Daybreaker", 16, Planks, 8, Metalbar, "Fort Sterling", "ARTEFACT_MAIN_SPEAR_LANCE_AVALON", "Ruined Ancestral Vamplate", 1, null, null, 0, Hunter, 250),
        new("2H_DUALSICKLE_UNDEAD", "Deathgivers", 16, Metalbar, 16, Leather, "Martlock", "ARTEFACT_2H_DUALSICKLE_UNDEAD", "Ghastly Blades", 1, null, null, 0, Hunter, 250),
        new("2H_TOOL_SIEGEHAMMER", "Demolition Hammer", 8, Planks, 8, Stoneblock, "Caerleon", null, null, 0, null, null, 0, Royal, 370),
        new("ARMOR_PLATE_HELL", "Demon Armor", 16, Metalbar, 0, null, "Bridgewatch", "ARTEFACT_ARMOR_PLATE_HELL", "Demonic Plates", 1, null, null, 0, Warrior, 250),
        new("SHOES_PLATE_HELL", "Demon Boots", 8, Metalbar, 0, null, "Martlock", "ARTEFACT_SHOES_PLATE_HELL", "Demonic Filling", 1, null, null, 0, Warrior, 250),
        new("HEAD_PLATE_HELL", "Demon Helmet", 8, Metalbar, 0, null, "Fort Sterling", "ARTEFACT_HEAD_PLATE_HELL", "Demonic Scraps", 1, null, null, 0, Warrior, 250),
        new("MAIN_DAGGER_HELL", "Demonfang", 12, Metalbar, 12, Leather, "Bridgewatch", "ARTEFACT_MAIN_DAGGER_HELL", "Broken Demonic Fang", 1, null, null, 0, Hunter, 250),
        new("2H_DEMONICSTAFF", "Demonic Staff", 20, Planks, 12, Metalbar, "Bridgewatch", null, null, 0, null, null, 0, Mage, 250),
        new("2H_DIVINESTAFF", "Divine Staff", 20, Planks, 12, Cloth, "Fort Sterling", null, null, 0, null, null, 0, Mage, 250),
        new("2H_DOUBLEBLADEDSTAFF", "Double Bladed Staff", 12, Metalbar, 20, Leather, "Martlock", null, null, 0, null, null, 0, Hunter, 250),
        new("HEAD_CLOTH_KEEPER", "Druid Cowl", 8, Cloth, 0, null, "Thetford", "ARTEFACT_HEAD_CLOTH_KEEPER", "Druidic Preserved Beak", 1, null, null, 0, Mage, 250),
        new("ARMOR_CLOTH_KEEPER", "Druid Robe", 16, Cloth, 0, null, "Fort Sterling", "ARTEFACT_ARMOR_CLOTH_KEEPER", "Druidic Feathers", 1, null, null, 0, Mage, 250),
        new("SHOES_CLOTH_KEEPER", "Druid Sandals", 8, Cloth, 0, null, "Thetford", "ARTEFACT_SHOES_CLOTH_KEEPER", "Druidic Bindings", 1, null, null, 0, Mage, 250),
        new("MAIN_NATURESTAFF_KEEPER", "Druidic Staff", 16, Planks, 8, Cloth, "Lymhurst", "ARTEFACT_MAIN_NATURESTAFF_KEEPER", "Druidic Inscriptions", 1, null, null, 0, Hunter, 250),
        new("2H_DUALSWORD", "Dual Swords", 20, Metalbar, 12, Leather, "Lymhurst", null, null, 0, null, null, 0, Warrior, 250),
        new("ARMOR_PLATE_FEY", "Duskweaver Armor", 16, Metalbar, 0, null, "Bridgewatch", "ARTEFACT_ARMOR_PLATE_FEY", "Veilweaver Carapace", 1, null, null, 0, Warrior, 250),
        new("SHOES_PLATE_FEY", "Duskweaver Boots", 8, Metalbar, 0, null, "Martlock", "ARTEFACT_SHOES_PLATE_FEY", "Veilweaver Claws", 1, null, null, 0, Warrior, 250),
        new("HEAD_PLATE_FEY", "Duskweaver Helmet", 8, Metalbar, 0, null, "Fort Sterling", "ARTEFACT_HEAD_PLATE_FEY", "Veilweaver Mandibles", 1, null, null, 0, Warrior, 250),
        new("2H_SHAPESHIFTER_KEEPER", "Earthrune Staff", 20, Planks, 12, Leather, "Caerleon", "ARTEFACT_2H_SHAPESHIFTER_KEEPER", "Runestone Golem Remnant", 1, "ALCHEMY_RARE_ELEMENTAL", "Runestone Tooth", 1, Hunter, 250),
        new("2H_CROSSBOW_CANNON_AVALON", "Energy Shaper", 20, Planks, 12, Metalbar, "Bridgewatch", "ARTEFACT_2H_CROSSBOW_CANNON_AVALON", "Humming Avalonian Whirligig", 1, null, null, 0, Warrior, 250),
        new("2H_ENIGMATICSTAFF", "Enigmatic Staff", 20, Planks, 12, Metalbar, "Martlock", null, null, 0, null, null, 0, Mage, 250),
        new("2H_ARCANE_RINGPAIR_AVALON", "Evensong", 20, Planks, 12, Metalbar, "Lymhurst", "ARTEFACT_2H_ARCANE_RINGPAIR_AVALON", "Hypnotic Harmonic Ring", 1, null, null, 0, Mage, 250),
        new("OFF_ORB_MORGANA", "Eye of Secrets", 4, Cloth, 4, Leather, "Martlock", "ARTEFACT_OFF_ORB_MORGANA", "Alluring Crystal", 1, null, null, 0, Mage, 250),
        new("OFF_SPIKEDSHIELD_MORGANA", "Facebreaker", 4, Planks, 4, Metalbar, "Fort Sterling", "ARTEFACT_OFF_SPIKEDSHIELD_MORGANA", "Bloodforged Spikes", 1, null, null, 0, Warrior, 250),
        new("2H_HOLYSTAFF_HELL", "Fallen Staff", 20, Planks, 12, Cloth, "Thetford", "ARTEFACT_2H_HOLYSTAFF_HELL", "Infernal Scroll", 1, null, null, 0, Mage, 250),
        new("HEAD_CLOTH_FEY", "Feyscale Hat", 8, Cloth, 0, null, "Thetford", "ARTEFACT_HEAD_CLOTH_FEY", "Intact Fey Fibula", 1, null, null, 0, Mage, 250),
        new("ARMOR_CLOTH_FEY", "Feyscale Robe", 16, Cloth, 0, null, "Fort Sterling", "ARTEFACT_ARMOR_CLOTH_FEY", "Fey Dorsal Wing", 1, null, null, 0, Mage, 250),
        new("SHOES_CLOTH_FEY", "Feyscale Sandals", 8, Cloth, 0, null, "Bridgewatch", "ARTEFACT_SHOES_CLOTH_FEY", "Fey Dragonscales", 1, null, null, 0, Mage, 250),
        new("HEAD_CLOTH_HELL", "Fiend Cowl", 8, Cloth, 0, null, "Thetford", "ARTEFACT_HEAD_CLOTH_HELL", "Infernal Cloth Visor", 1, null, null, 0, Mage, 250),
        new("ARMOR_CLOTH_HELL", "Fiend Robe", 16, Cloth, 0, null, "Fort Sterling", "ARTEFACT_ARMOR_CLOTH_HELL", "Infernal Cloth Folds", 1, null, null, 0, Mage, 250),
        new("SHOES_CLOTH_HELL", "Fiend Sandals", 8, Cloth, 0, null, "Bridgewatch", "ARTEFACT_SHOES_CLOTH_HELL", "Infernal Cloth Bindings", 1, null, null, 0, Mage, 250),
        new("MAIN_FIRESTAFF", "Fire Staff", 16, Planks, 8, Metalbar, "Thetford", null, null, 0, null, null, 0, Mage, 250),
        new("BACKPACK_GATHERER_FISH", "Fisherman Backpack", 4, Cloth, 4, Leather, "None", null, null, 0, null, null, 0, Royal, 0),
        new("HEAD_GATHERER_FISH", "Fisherman Cap", 8, Leather, 0, null, "None", null, null, 0, null, null, 0, Royal, 250),
        new("ARMOR_GATHERER_FISH", "Fisherman Garb", 16, Leather, 0, null, "None", null, null, 0, null, null, 0, Royal, 250),
        new("SHOES_GATHERER_FISH", "Fisherman Workboots", 8, Leather, 0, null, "None", null, null, 0, null, null, 0, Royal, 250),
        new("2H_TOOL_FISHINGROD", "Fishing Rod", 6, Planks, 2, Cloth, "Caerleon", null, null, 0, null, null, 0, Royal, 250),
        new("2H_KNUCKLES_AVALON", "Fists of Avalon", 20, Leather, 12, Metalbar, "Caerleon", "ARTEFACT_2H_KNUCKLES_AVALON", "Damaged Avalonian Gauntlet", 1, null, null, 0, Warrior, 250),
        new("2H_DUALHAMMER_HELL", "Forge Hammers", 20, Metalbar, 12, Cloth, "Fort Sterling", "ARTEFACT_2H_DUALHAMMER_HELL", "Hellish Hammer Heads", 1, null, null, 0, Warrior, 250),
        new("MAIN_FROSTSTAFF", "Frost Staff", 16, Planks, 8, Metalbar, "Martlock", null, null, 0, null, null, 0, Mage, 250),
        new("2H_DUALSCIMITAR_UNDEAD", "Galatine Pair", 20, Metalbar, 12, Leather, "Lymhurst", "ARTEFACT_2H_DUALSCIMITAR_UNDEAD", "Cursed Blades", 1, null, null, 0, Warrior, 250),
        new("2H_GLACIALSTAFF", "Glacial Staff", 20, Planks, 12, Metalbar, "Martlock", null, null, 0, null, null, 0, Mage, 250),
        new("2H_GLAIVE", "Glaive", 12, Planks, 20, Metalbar, "Fort Sterling", null, null, 0, null, null, 0, Hunter, 250),
        new("2H_QUARTERSTAFF_AVALON", "Grailseeker", 20, Leather, 12, Metalbar, "Martlock", "ARTEFACT_2H_QUARTERSTAFF_AVALON", "Timeworn Walking Staff", 1, null, null, 0, Hunter, 250),
        new("ARMOR_PLATE_UNDEAD", "Graveguard Armor", 16, Metalbar, 0, null, "Bridgewatch", "ARTEFACT_ARMOR_PLATE_UNDEAD", "Ancient Chain Rings", 1, null, null, 0, Warrior, 250),
        new("SHOES_PLATE_UNDEAD", "Graveguard Boots", 8, Metalbar, 0, null, "Martlock", "ARTEFACT_SHOES_PLATE_UNDEAD", "Ancient Bindings", 1, null, null, 0, Warrior, 250),
        new("HEAD_PLATE_UNDEAD", "Graveguard Helmet", 8, Metalbar, 0, null, "Fort Sterling", "ARTEFACT_HEAD_PLATE_UNDEAD", "Ancient Padding", 1, null, null, 0, Warrior, 250),
        new("2H_ARCANESTAFF", "Great Arcane Staff", 20, Planks, 12, Metalbar, "Lymhurst", null, null, 0, null, null, 0, Mage, 250),
        new("2H_CURSEDSTAFF", "Great Cursed Staff", 20, Planks, 12, Metalbar, "Bridgewatch", null, null, 0, null, null, 0, Mage, 250),
        new("2H_FIRESTAFF", "Great Fire Staff", 20, Planks, 12, Metalbar, "Thetford", null, null, 0, null, null, 0, Mage, 250),
        new("2H_FROSTSTAFF", "Great Frost Staff", 20, Planks, 12, Metalbar, "Martlock", null, null, 0, null, null, 0, Mage, 250),
        new("2H_HAMMER", "Great Hammer", 20, Metalbar, 12, Cloth, "Fort Sterling", null, null, 0, null, null, 0, Warrior, 250),
        new("2H_HOLYSTAFF", "Great Holy Staff", 20, Planks, 12, Cloth, "Fort Sterling", null, null, 0, null, null, 0, Mage, 250),
        new("2H_NATURESTAFF", "Great Nature Staff", 20, Planks, 12, Cloth, "Thetford", null, null, 0, null, null, 0, Hunter, 250),
        new("2H_AXE", "Greataxe", 12, Planks, 20, Metalbar, "Martlock", null, null, 0, null, null, 0, Warrior, 250),
        new("2H_RAM_KEEPER", "Grovekeeper", 20, Metalbar, 12, Cloth, "Fort Sterling", "ARTEFACT_2H_RAM_KEEPER", "Engraved Log", 1, null, null, 0, Warrior, 250),
        new("ARMOR_PLATE_SET3", "Guardian Armor", 16, Metalbar, 0, null, "Bridgewatch", null, null, 0, null, null, 0, Warrior, 250),
        new("SHOES_PLATE_SET3", "Guardian Boots", 8, Metalbar, 0, null, "Martlock", null, null, 0, null, null, 0, Warrior, 250),
        new("HEAD_PLATE_SET3", "Guardian Helmet", 8, Metalbar, 0, null, "Fort Sterling", null, null, 0, null, null, 0, Warrior, 250),
        new("2H_HALBERD", "Halberd", 20, Planks, 12, Metalbar, "Martlock", null, null, 0, null, null, 0, Warrior, 250),
        new("MAIN_HOLYSTAFF_AVALON", "Hallowfall", 16, Planks, 8, Cloth, "Fort Sterling", "ARTEFACT_MAIN_HOLYSTAFF_AVALON", "Messianic Curio", 1, null, null, 0, Mage, 250),
        new("MAIN_HAMMER", "Hammer", 24, Metalbar, 0, null, "Fort Sterling", null, null, 0, null, null, 0, Warrior, 250),
        new("2H_HAMMER_AVALON", "Hand of Justice", 20, Metalbar, 12, Cloth, "Fort Sterling", "ARTEFACT_2H_HAMMER_AVALON", "Massive Metallic Hand", 1, null, null, 0, Warrior, 250),
        new("BACKPACK_GATHERER_FIBER", "Harvester Backpack", 4, Cloth, 4, Leather, "None", null, null, 0, null, null, 0, Royal, 0),
        new("HEAD_GATHERER_FIBER", "Harvester Cap", 8, Cloth, 0, null, "None", null, null, 0, null, null, 0, Royal, 250),
        new("ARMOR_GATHERER_FIBER", "Harvester Garb", 16, Cloth, 0, null, "None", null, null, 0, null, null, 0, Royal, 250),
        new("SHOES_GATHERER_FIBER", "Harvester Workboots", 8, Cloth, 0, null, "None", null, null, 0, null, null, 0, Royal, 250),
        new("2H_CROSSBOWLARGE", "Heavy Crossbow", 20, Planks, 12, Metalbar, "Bridgewatch", null, null, 0, null, null, 0, Warrior, 250),
        new("2H_MACE", "Heavy Mace", 20, Metalbar, 12, Cloth, "Thetford", null, null, 0, null, null, 0, Warrior, 250),
        new("2H_KNUCKLES_HELL", "Hellfire Hands", 20, Leather, 12, Metalbar, "Caerleon", "ARTEFACT_2H_KNUCKLES_HELL", "Severed Demonic Horns", 1, null, null, 0, Warrior, 250),
        new("HEAD_LEATHER_HELL", "Hellion Hood", 8, Leather, 0, null, "Lymhurst", "ARTEFACT_HEAD_LEATHER_HELL", "Demonhide Padding", 1, null, null, 0, Hunter, 250),
        new("ARMOR_LEATHER_HELL", "Hellion Jacket", 16, Leather, 0, null, "Thetford", "ARTEFACT_ARMOR_LEATHER_HELL", "Demonhide Leather", 1, null, null, 0, Hunter, 250),
        new("SHOES_LEATHER_HELL", "Hellion Shoes", 8, Leather, 0, null, "Lymhurst", "ARTEFACT_SHOES_LEATHER_HELL", "Demonhide Bindings", 1, null, null, 0, Hunter, 250),
        new("2H_SHAPESHIFTER_HELL", "Hellspawn Staff", 20, Planks, 12, Leather, "Caerleon", "ARTEFACT_2H_SHAPESHIFTER_HELL", "Hellfire Imp Remnant", 1, "ALCHEMY_RARE_IMP", "Imp's Horn", 1, Hunter, 250),
        new("HEAD_PLATE_AVALON", "Helmet of Valor", 8, Metalbar, 0, null, "Fort Sterling", "ARTEFACT_HEAD_PLATE_AVALON", "Exalted Visor", 1, null, null, 0, Warrior, 250),
        new("MAIN_SPEAR_KEEPER", "Heron Spear", 16, Planks, 8, Metalbar, "Fort Sterling", "ARTEFACT_MAIN_SPEAR_KEEPER", "Keeper Spearhead", 1, null, null, 0, Hunter, 250),
        new("MAIN_FROSTSTAFF_KEEPER", "Hoarfrost Staff", 16, Planks, 8, Metalbar, "Martlock", "ARTEFACT_MAIN_FROSTSTAFF_KEEPER", "Hoarfrost Orb", 1, null, null, 0, Mage, 250),
        new("MAIN_HOLYSTAFF", "Holy Staff", 16, Planks, 8, Cloth, "Fort Sterling", null, null, 0, null, null, 0, Mage, 250),
        new("HEAD_LEATHER_AVALON", "Hood of Tenacity", 8, Leather, 0, null, "Lymhurst", "ARTEFACT_HEAD_LEATHER_AVALON", "Augured Padding", 1, null, null, 0, Hunter, 250),
        new("HEAD_LEATHER_SET2", "Hunter Hood", 8, Leather, 0, null, "Lymhurst", null, null, 0, null, null, 0, Hunter, 250),
        new("ARMOR_LEATHER_SET2", "Hunter Jacket", 16, Leather, 0, null, "Thetford", null, null, 0, null, null, 0, Hunter, 250),
        new("SHOES_LEATHER_SET2", "Hunter Shoes", 8, Leather, 0, null, "Lymhurst", null, null, 0, null, null, 0, Hunter, 250),
        new("2H_ICEGAUNTLETS_HELL", "Icicle Staff", 20, Planks, 12, Metalbar, "Martlock", "ARTEFACT_2H_ICEGAUNTLETS_HELL", "Icicle Orb", 1, null, null, 0, Mage, 250),
        new("MAIN_MACE_HELL", "Incubus Mace", 16, Metalbar, 8, Cloth, "Thetford", "ARTEFACT_MAIN_MACE_HELL", "Infernal Mace Head", 1, null, null, 0, Warrior, 250),
        new("2H_SCYTHE_HELL", "Infernal Scythe", 12, Planks, 20, Metalbar, "Martlock", "ARTEFACT_2H_SCYTHE_HELL", "Hellish Sicklehead", 1, null, null, 0, Warrior, 250),
        new("2H_INFERNOSTAFF", "Infernal Staff", 20, Planks, 12, Metalbar, "Thetford", null, null, 0, null, null, 0, Mage, 250),
        new("MAIN_SWORD_CRYSTAL", "Infinity Blade", 16, Metalbar, 8, Leather, "Lymhurst", "ARTEFACT_MAIN_SWORD_CRYSTAL", "Infinite Crystal", 1, null, null, 0, Warrior, 250),
        new("2H_IRONCLADEDSTAFF", "Iron-clad Staff", 12, Metalbar, 20, Leather, "Martlock", null, null, 0, null, null, 0, Hunter, 250),
        new("MAIN_NATURESTAFF_AVALON", "Ironroot Staff", 16, Planks, 8, Cloth, "Thetford", "ARTEFACT_MAIN_NATURESTAFF_AVALON", "Uprooted Perennial Sapling", 1, null, null, 0, Hunter, 250),
        new("ARMOR_LEATHER_AVALON", "Jacket of Tenacity", 16, Leather, 0, null, "Thetford", "ARTEFACT_ARMOR_LEATHER_AVALON", "Augured Sash", 1, null, null, 0, Hunter, 250),
        new("ARMOR_PLATE_KEEPER", "Judicator Armor", 16, Metalbar, 0, null, "Bridgewatch", "ARTEFACT_ARMOR_PLATE_KEEPER", "Preserved Animal Fur", 1, null, null, 0, Warrior, 250),
        new("SHOES_PLATE_KEEPER", "Judicator Boots", 8, Metalbar, 0, null, "Martlock", "ARTEFACT_SHOES_PLATE_KEEPER", "Inscribed Bindings", 1, null, null, 0, Warrior, 250),
        new("HEAD_PLATE_KEEPER", "Judicator Helmet", 8, Metalbar, 0, null, "Fort Sterling", "ARTEFACT_HEAD_PLATE_KEEPER", "Carved Skull Padding", 1, null, null, 0, Warrior, 250),
        new("2H_CLAYMORE_AVALON", "Kingmaker", 20, Metalbar, 12, Leather, "Lymhurst", "ARTEFACT_2H_CLAYMORE_AVALON", "Remnants of the Old King", 1, null, null, 0, Warrior, 250),
        new("ARMOR_PLATE_SET2", "Knight Armor", 16, Metalbar, 0, null, "Bridgewatch", null, null, 0, null, null, 0, Warrior, 250),
        new("SHOES_PLATE_SET2", "Knight Boots", 8, Metalbar, 0, null, "Martlock", null, null, 0, null, null, 0, Warrior, 250),
        new("HEAD_PLATE_SET2", "Knight Helmet", 8, Metalbar, 0, null, "Fort Sterling", null, null, 0, null, null, 0, Warrior, 250),
        new("OFF_JESTERCANE_HELL", "Leering Cane", 4, Planks, 4, Cloth, "Martlock", "ARTEFACT_OFF_JESTERCANE_HELL", "Hellish Handle", 1, null, null, 0, Hunter, 250),
        new("MAIN_CURSEDSTAFF_UNDEAD", "Lifecurse Staff", 16, Planks, 8, Metalbar, "Bridgewatch", "ARTEFACT_MAIN_CURSEDSTAFF_UNDEAD", "Lost Cursed Crystal", 1, null, null, 0, Mage, 250),
        new("MAIN_HOLYSTAFF_MORGANA", "Lifetouch Staff", 16, Planks, 8, Cloth, "Fort Sterling", "ARTEFACT_MAIN_HOLYSTAFF_MORGANA", "Possessed Scroll", 1, null, null, 0, Mage, 250),
        new("2H_SHAPESHIFTER_AVALON", "Lightcaller", 20, Planks, 12, Leather, "Caerleon", "ARTEFACT_2H_SHAPESHIFTER_AVALON", "Dawnbird Remnant", 1, "ALCHEMY_RARE_EAGLE", "Dawnfeather", 1, Hunter, 250),
        new("MAIN_1HCROSSBOW", "Light Crossbow", 16, Planks, 8, Metalbar, "Bridgewatch", null, null, 0, null, null, 0, Warrior, 250),
        new("2H_LONGBOW", "Longbow", 32, Planks, 0, null, "Lymhurst", null, null, 0, null, null, 0, Hunter, 250),
        new("BACKPACK_GATHERER_WOOD", "Lumberjack Backpack", 4, Cloth, 4, Leather, "None", null, null, 0, null, null, 0, Royal, 0),
        new("HEAD_GATHERER_WOOD", "Lumberjack Cap", 8, Leather, 0, null, "None", null, null, 0, null, null, 0, Royal, 250),
        new("ARMOR_GATHERER_WOOD", "Lumberjack Garb", 16, Leather, 0, null, "None", null, null, 0, null, null, 0, Royal, 250),
        new("SHOES_GATHERER_WOOD", "Lumberjack Workboots", 8, Leather, 0, null, "None", null, null, 0, null, null, 0, Royal, 250),
        new("MAIN_MACE", "Mace", 16, Metalbar, 8, Cloth, "Thetford", null, null, 0, null, null, 0, Warrior, 250),
        new("HEAD_CLOTH_SET3", "Mage Cowl", 8, Cloth, 0, null, "Thetford", null, null, 0, null, null, 0, Mage, 250),
        new("ARMOR_CLOTH_SET3", "Mage Robe", 16, Cloth, 0, null, "Fort Sterling", null, null, 0, null, null, 0, Mage, 250),
        new("SHOES_CLOTH_SET3", "Mage Sandals", 8, Cloth, 0, null, "Bridgewatch", null, null, 0, null, null, 0, Mage, 250),
        new("2H_ENIGMATICORB_MORGANA", "Malevolent Locus", 20, Planks, 12, Metalbar, "Lymhurst", "ARTEFACT_2H_ENIGMATICORB_MORGANA", "Possessed Catalyst", 1, null, null, 0, Mage, 250),
        new("HEAD_LEATHER_SET1", "Mercenary Hood", 8, Leather, 0, null, "Lymhurst", null, null, 0, null, null, 0, Hunter, 250),
        new("ARMOR_LEATHER_SET1", "Mercenary Jacket", 16, Leather, 0, null, "Thetford", null, null, 0, null, null, 0, Hunter, 250),
        new("SHOES_LEATHER_SET1", "Mercenary Shoes", 8, Leather, 0, null, "Lymhurst", null, null, 0, null, null, 0, Hunter, 250),
        new("BACKPACK_GATHERER_ORE", "Miner Backpack", 4, Cloth, 4, Leather, "None", null, null, 0, null, null, 0, Royal, 0),
        new("HEAD_GATHERER_ORE", "Miner Cap", 8, Metalbar, 0, null, "None", null, null, 0, null, null, 0, Royal, 250),
        new("ARMOR_GATHERER_ORE", "Miner Garb", 16, Metalbar, 0, null, "None", null, null, 0, null, null, 0, Royal, 250),
        new("SHOES_GATHERER_ORE", "Miner Workboots", 8, Metalbar, 0, null, "None", null, null, 0, null, null, 0, Royal, 250),
        new("OFF_HORN_KEEPER", "Mistcaller", 4, Planks, 4, Cloth, "Martlock", "ARTEFACT_OFF_HORN_KEEPER", "Runed Horn", 1, null, null, 0, Hunter, 250),
        new("2H_BOW_AVALON", "Mistpiercer", 32, Planks, 0, null, "Lymhurst", "ARTEFACT_2H_BOW_AVALON", "Immaculately Crafted Riser", 1, null, null, 0, Hunter, 250),
        new("HEAD_LEATHER_FEY", "Mistwalker Hood", 8, Leather, 0, null, "Lymhurst", "ARTEFACT_HEAD_LEATHER_FEY", "Flawless Griffin Beak", 1, null, null, 0, Hunter, 250),
        new("ARMOR_LEATHER_FEY", "Mistwalker Jacket", 16, Leather, 0, null, "Thetford", "ARTEFACT_ARMOR_LEATHER_FEY", "Untarnished Griffin Feathers", 1, null, null, 0, Hunter, 250),
        new("SHOES_LEATHER_FEY", "Mistwalker Shoes", 8, Leather, 0, null, "Lymhurst", "ARTEFACT_SHOES_LEATHER_FEY", "Griffin Underfur", 1, null, null, 0, Hunter, 250),
        new("2H_FLAIL", "Morning Star", 20, Metalbar, 12, Cloth, "Thetford", null, null, 0, null, null, 0, Warrior, 250),
        new("OFF_DEMONSKULL_HELL", "Muisak", 4, Cloth, 4, Leather, "Martlock", "ARTEFACT_OFF_DEMONSKULL_HELL", "Demonic Jawbone", 1, null, null, 0, Mage, 250),
        new("MAIN_NATURESTAFF", "Nature Staff", 16, Planks, 8, Cloth, "Thetford", null, null, 0, null, null, 0, Hunter, 250),
        new("2H_DUALMACE_AVALON", "Oathkeepers", 20, Metalbar, 12, Cloth, "Thetford", "ARTEFACT_2H_DUALMACE_AVALON", "Broken Oaths", 1, null, null, 0, Warrior, 250),
        new("2H_ARCANESTAFF_HELL", "Occult Staff", 20, Planks, 12, Metalbar, "Lymhurst", "ARTEFACT_2H_ARCANESTAFF_HELL", "Occult Orb", 1, null, null, 0, Mage, 250),
        new("2H_ICECRYSTAL_UNDEAD", "Permafrost Prism", 20, Planks, 12, Metalbar, "Martlock", "ARTEFACT_2H_ICECRYSTAL_UNDEAD", "Cursed Frozen Crystal", 1, null, null, 0, Mage, 250),
        new("2H_TOOL_PICK", "Pickaxe", 6, Planks, 2, Metalbar, "Caerleon", null, null, 0, null, null, 0, Royal, 250),
        new("2H_SPEAR", "Pike", 20, Planks, 12, Metalbar, "Fort Sterling", null, null, 0, null, null, 0, Hunter, 250),
        new("2H_POLEHAMMER", "Polehammer", 20, Metalbar, 12, Cloth, "Fort Sterling", null, null, 0, null, null, 0, Warrior, 250),
        new("2H_SHAPESHIFTER_SET3", "Primal Staff", 20, Planks, 12, Leather, "Caerleon", null, null, 0, "ALCHEMY_RARE_DIREBEAR", "Spirit Paws", 1, Hunter, 250),
        new("2H_SHAPESHIFTER_SET1", "Prowling Staff", 20, Planks, 12, Leather, "Caerleon", null, null, 0, "ALCHEMY_RARE_PANTHER", "Shadow Claws", 1, Hunter, 250),
        new("BACKPACK_GATHERER_ROCK", "Quarrier Backpack", 4, Cloth, 4, Leather, "None", null, null, 0, null, null, 0, Royal, 0),
        new("HEAD_GATHERER_ROCK", "Quarrier Cap", 8, Metalbar, 0, null, "None", null, null, 0, null, null, 0, Royal, 250),
        new("ARMOR_GATHERER_ROCK", "Quarrier Garb", 16, Metalbar, 0, null, "None", null, null, 0, null, null, 0, Royal, 250),
        new("SHOES_GATHERER_ROCK", "Quarrier Workboots", 8, Metalbar, 0, null, "None", null, null, 0, null, null, 0, Royal, 250),
        new("2H_QUARTERSTAFF", "Quarterstaff", 12, Metalbar, 20, Leather, "Martlock", null, null, 0, null, null, 0, Hunter, 250),
        new("2H_NATURESTAFF_KEEPER", "Rampant Staff", 20, Planks, 12, Cloth, "Thetford", "ARTEFACT_2H_NATURESTAFF_KEEPER", "Preserved Log", 1, null, null, 0, Hunter, 250),
        new("2H_KNUCKLES_MORGANA", "Ravenstrike Cestus", 20, Leather, 12, Metalbar, "Caerleon", "ARTEFACT_2H_KNUCKLES_MORGANA", "Warped Raven Plate", 1, null, null, 0, Warrior, 250),
        new("2H_AXE_AVALON", "Realmbreaker", 20, Metalbar, 12, Planks, "Martlock", "ARTEFACT_2H_AXE_AVALON", "Avalonian Battle Memoir", 1, null, null, 0, Warrior, 250),
        new("2H_HOLYSTAFF_UNDEAD", "Redemption Staff", 20, Planks, 12, Cloth, "Fort Sterling", "ARTEFACT_2H_HOLYSTAFF_UNDEAD", "Ghastly Scroll", 1, null, null, 0, Mage, 250),
        new("ARMOR_CLOTH_AVALON", "Robe of Purity", 16, Cloth, 0, null, "Fort Sterling", "ARTEFACT_ARMOR_CLOTH_AVALON", "Sanctified Belt", 1, null, null, 0, Mage, 250),
        new("2H_SHAPESHIFTER_SET2", "Rootbound Staff", 20, Planks, 12, Leather, "Caerleon", null, null, 0, "ALCHEMY_RARE_ENT", "Sylvian Root", 1, Hunter, 250),
        new("OFF_TALISMAN_AVALON", "Sacred Scepter", 4, Planks, 4, Cloth, "Martlock", "ARTEFACT_OFF_TALISMAN_AVALON", "Shattered Avalonian Memento", 1, null, null, 0, Mage, 250),
        new("SHOES_CLOTH_AVALON", "Sandals of Purity", 8, Cloth, 0, null, "Bridgewatch", "ARTEFACT_SHOES_CLOTH_AVALON", "Sanctified Bindings", 1, null, null, 0, Mage, 250),
        new("OFF_TOWERSHIELD_UNDEAD", "Sarcophagus", 4, Planks, 4, Metalbar, "Martlock", "ARTEFACT_OFF_TOWERSHIELD_UNDEAD", "Ancient Shield Core", 1, null, null, 0, Warrior, 250),
        new("BAG_INSIGHT", "Satchel of Insight", 8, Cloth, 8, Leather, "Brecilien", "T4_SKILLBOOK_STANDARD", "Tome of Insight (T4)", 1, null, null, 0, Royal, 310),
        new("HEAD_CLOTH_SET1", "Scholar Cowl", 8, Cloth, 0, null, "Thetford", null, null, 0, null, null, 0, Mage, 250),
        new("ARMOR_CLOTH_SET1", "Scholar Robe", 16, Cloth, 0, null, "Fort Sterling", null, null, 0, null, null, 0, Mage, 250),
        new("SHOES_CLOTH_SET1", "Scholar Sandals", 8, Cloth, 0, null, "Bridgewatch", null, null, 0, null, null, 0, Mage, 250),
        new("MAIN_CURSEDSTAFF_AVALON", "Shadowcaller", 16, Planks, 8, Metalbar, "Bridgewatch", "ARTEFACT_MAIN_CURSEDSTAFF_AVALON", "Fractured Opaque Orb", 1, null, null, 0, Mage, 250),
        new("OFF_SHIELD", "Shield", 4, Planks, 4, Metalbar, "Martlock", null, null, 0, null, null, 0, Warrior, 250),
        new("SHOES_LEATHER_AVALON", "Shoes of Tenacity", 8, Leather, 0, null, "Lymhurst", "ARTEFACT_SHOES_LEATHER_AVALON", "Augured Fasteners", 1, null, null, 0, Hunter, 250),
        new("2H_TOOL_SICKLE", "Sickle", 6, Planks, 2, Metalbar, "Caerleon", null, null, 0, null, null, 0, Royal, 250),
        new("2H_CROSSBOWLARGE_MORGANA", "Siegebow", 20, Planks, 12, Metalbar, "Bridgewatch", "ARTEFACT_2H_CROSSBOWLARGE_MORGANA", "Alluring Bolts", 1, null, null, 0, Warrior, 250),
        new("BACKPACK_GATHERER_HIDE", "Skinner Backpack", 4, Cloth, 4, Leather, "None", null, null, 0, null, null, 0, Royal, 0),
        new("HEAD_GATHERER_HIDE", "Skinner Cap", 8, Leather, 0, null, "None", null, null, 0, null, null, 0, Royal, 250),
        new("ARMOR_GATHERER_HIDE", "Skinner Garb", 16, Leather, 0, null, "None", null, null, 0, null, null, 0, Royal, 250),
        new("SHOES_GATHERER_HIDE", "Skinner Workboots", 8, Leather, 0, null, "None", null, null, 0, null, null, 0, Royal, 250),
        new("2H_TOOL_KNIFE", "Skinning Knife", 6, Planks, 2, Metalbar, "Caerleon", null, null, 0, null, null, 0, Royal, 250),
        new("ARMOR_PLATE_SET1", "Soldier Armor", 16, Metalbar, 0, null, "Bridgewatch", null, null, 0, null, null, 0, Warrior, 250),
        new("SHOES_PLATE_SET1", "Soldier Boots", 8, Metalbar, 0, null, "Martlock", null, null, 0, null, null, 0, Warrior, 250),
        new("HEAD_PLATE_SET1", "Soldier Helmet", 8, Metalbar, 0, null, "Fort Sterling", null, null, 0, null, null, 0, Warrior, 250),
        new("2H_TWINSCYTHE_HELL", "Soulscythe", 12, Metalbar, 20, Leather, "Martlock", "ARTEFACT_2H_TWINSCYTHE_HELL", "Hellish Sicklehead Pair", 1, null, null, 0, Hunter, 250),
        new("MAIN_SPEAR", "Spear", 16, Planks, 8, Metalbar, "Fort Sterling", null, null, 0, null, null, 0, Hunter, 250),
        new("HEAD_LEATHER_UNDEAD", "Specter Hood", 8, Leather, 0, null, "Lymhurst", "ARTEFACT_HEAD_LEATHER_UNDEAD", "Ghastly Visor", 1, null, null, 0, Hunter, 250),
        new("ARMOR_LEATHER_UNDEAD", "Specter Jacket", 16, Leather, 0, null, "Thetford", "ARTEFACT_ARMOR_LEATHER_UNDEAD", "Ghastly Leather", 1, null, null, 0, Hunter, 250),
        new("SHOES_LEATHER_UNDEAD", "Specter Shoes", 8, Leather, 0, null, "Lymhurst", "ARTEFACT_SHOES_LEATHER_UNDEAD", "Ghastly Bindings", 1, null, null, 0, Hunter, 250),
        new("2H_KNUCKLES_SET3", "Spiked Gauntlets", 20, Leather, 12, Metalbar, "Caerleon", null, null, 0, null, null, 0, Warrior, 250),
        new("2H_HARPOON_HELL", "Spirithunter", 20, Planks, 12, Metalbar, "Fort Sterling", "ARTEFACT_2H_HARPOON_HELL", "Infernal Harpoon Tip", 1, null, null, 0, Hunter, 250),
        new("2H_ROCKSTAFF_KEEPER", "Staff of Balance", 12, Metalbar, 20, Leather, "Martlock", "ARTEFACT_2H_ROCKSTAFF_KEEPER", "Preserved Rocks", 1, null, null, 0, Hunter, 250),
        new("HEAD_LEATHER_MORGANA", "Stalker Hood", 8, Leather, 0, null, "Lymhurst", "ARTEFACT_HEAD_LEATHER_MORGANA", "Imbued Visor", 1, null, null, 0, Hunter, 250),
        new("ARMOR_LEATHER_MORGANA", "Stalker Jacket", 16, Leather, 0, null, "Thetford", "ARTEFACT_ARMOR_LEATHER_MORGANA", "Imbued Leather Folds", 1, null, null, 0, Hunter, 250),
        new("SHOES_LEATHER_MORGANA", "Stalker Shoes", 8, Leather, 0, null, "Lymhurst", "ARTEFACT_SHOES_LEATHER_MORGANA", "Imbued Soles", 1, null, null, 0, Hunter, 250),
        new("2H_TOOL_HAMMER", "Stone Hammer", 6, Planks, 2, Metalbar, "Caerleon", null, null, 0, null, null, 0, Royal, 250),
        new("OFF_TOTEM_KEEPER", "Taproot", 4, Cloth, 4, Leather, "Martlock", "ARTEFACT_OFF_TOTEM_KEEPER", "Inscribed Stone", 1, null, null, 0, Mage, 250),
        new("2H_HAMMER_UNDEAD", "Tombhammer", 20, Metalbar, 12, Cloth, "Fort Sterling", "ARTEFACT_2H_HAMMER_UNDEAD", "Ancient Hammer Head", 1, null, null, 0, Warrior, 250),
        new("OFF_BOOK", "Tome of Spells", 4, Cloth, 4, Leather, "Martlock", null, null, 0, null, null, 0, Mage, 250),
        new("OFF_TORCH", "Torch", 4, Planks, 4, Cloth, "Martlock", null, null, 0, null, null, 0, Hunter, 250),
        new("2H_TOOL_TRACKING", "Tracking Toolkit", 2, Planks, 6, Leather, "Caerleon", null, null, 0, null, null, 0, Royal, 250),
        new("2H_TRIDENT_UNDEAD", "Trinity Spear", 20, Planks, 12, Metalbar, "Fort Sterling", "ARTEFACT_2H_TRIDENT_UNDEAD", "Cursed Barbs", 1, null, null, 0, Hunter, 250),
        new("2H_KNUCKLES_KEEPER", "Ursine Maulers", 20, Leather, 12, Metalbar, "Caerleon", "ARTEFACT_2H_KNUCKLES_KEEPER", "Ursine Guardian Remains", 1, null, null, 0, Warrior, 250),
        new("2H_BOW_HELL", "Wailing Bow", 32, Planks, 0, null, "Lymhurst", "ARTEFACT_2H_BOW_HELL", "Demonic Arrowheads", 1, null, null, 0, Hunter, 250),
        new("2H_WARBOW", "Warbow", 32, Planks, 0, null, "Lymhurst", null, null, 0, null, null, 0, Hunter, 250),
        new("2H_REPEATINGCROSSBOW_UNDEAD", "Weeping Repeater", 20, Planks, 12, Metalbar, "Bridgewatch", "ARTEFACT_2H_REPEATINGCROSSBOW_UNDEAD", "Lost Crossbow Mechanism", 1, null, null, 0, Warrior, 250),
        new("2H_LONGBOW_UNDEAD", "Whispering Bow", 32, Planks, 0, null, "Lymhurst", "ARTEFACT_2H_LONGBOW_UNDEAD", "Ghastly Arrows", 1, null, null, 0, Hunter, 250),
        new("2H_WILDSTAFF", "Wild Staff", 20, Planks, 12, Cloth, "Thetford", null, null, 0, null, null, 0, Hunter, 250),
        new("MAIN_FIRESTAFF_KEEPER", "Wildfire Staff", 16, Planks, 8, Metalbar, "Thetford", "ARTEFACT_MAIN_FIRESTAFF_KEEPER", "Wildfire Orb", 1, null, null, 0, Mage, 250),
        new("MAIN_ARCANESTAFF_UNDEAD", "Witchwork Staff", 16, Planks, 8, Metalbar, "Lymhurst", "ARTEFACT_MAIN_ARCANESTAFF_UNDEAD", "Lost Arcane Crystal", 1, null, null, 0, Mage, 250),
        new("2H_GLAIVE_CRYSTAL", "Rift Glaive", 12, Planks, 20, Metalbar, "Fort Sterling", "ARTEFACT_2H_GLAIVE_CRYSTAL", "Rift Crystal", 1, null, null, 0, Hunter, 250),
        new("2H_DOUBLEBLADEDSTAFF_CRYSTAL", "Phantom Twinblade", 20, Leather, 12, Metalbar, "Martlock", "ARTEFACT_2H_DOUBLEBLADEDSTAFF_CRYSTAL", "Mirage Crystal", 1, null, null, 0, Hunter, 250),
        new("2H_FROSTSTAFF_CRYSTAL", "Arctic Staff", 20, Planks, 12, Metalbar, "Martlock", "ARTEFACT_2H_FROSTSTAFF_CRYSTAL", "Icy Crystal", 1, null, null, 0, Mage, 250),
        new("2H_ARCANESTAFF_CRYSTAL", "Astral Staff", 20, Planks, 12, Metalbar, "Lymhurst", "ARTEFACT_2H_ARCANESTAFF_CRYSTAL", "Startouched Crystal", 1, null, null, 0, Mage, 250),
        new("2H_HOLYSTAFF_CRYSTAL", "Exalted Staff", 20, Planks, 12, Cloth, "Fort Sterling", "ARTEFACT_2H_HOLYSTAFF_CRYSTAL", "Exalted Crystal", 1, null, null, 0, Mage, 250),
        new("2H_DAGGERPAIR_CRYSTAL", "Twin Slayers", 16, Leather, 16, Metalbar, "Bridgewatch", "ARTEFACT_2H_DAGGERPAIR_CRYSTAL", "Death-Touched Crystal", 1, null, null, 0, Hunter, 250),
        new("2H_SCYTHE_CRYSTAL", "Crystal Reaper", 12, Planks, 20, Metalbar, "Martlock", "ARTEFACT_2H_SCYTHE_CRYSTAL", "Edged Crystal", 1, null, null, 0, Warrior, 250),
        new("MAIN_MACE_CRYSTAL", "Dreadstorm Monarch", 16, Metalbar, 8, Cloth, "Thetford", "ARTEFACT_MAIN_MACE_CRYSTAL", "Dreadstorm Crystal", 1, null, null, 0, Warrior, 250),
    };

    // Tier quantity multipliers (T4=1x, T5=2x, T6=4x, T7=8x, T8=16x)
    public static readonly IReadOnlyDictionary<int, int> TierQtyMultiplier = new Dictionary<int, int>
    {
        [4] = 1, [5] = 2, [6] = 4, [7] = 8, [8] = 16
    };

    // Fame multiplier per tier/enchant combination
    // Base: T4.0=22.5, scales ×4 per tier, ×2 per enchant
    private static readonly decimal[] TierBaseFame = { 22.5m, 90m, 270m, 810m, 2430m };

    public static decimal GetFameMultiplier(int tier, int enchant)
    {
        var tierIndex = tier - 4;
        if (tierIndex < 0 || tierIndex >= TierBaseFame.Length) return 0;
        return TierBaseFame[tierIndex] * (decimal)Math.Pow(2, enchant);
    }

    // Build the full Albion Online API item ID
    public static string GetItemApiId(GearItem item, int tier, int enchant)
    {
        var enchantSuffix = enchant > 0 ? $"@{enchant}" : "";
        return $"T{tier}_{item.Id}{enchantSuffix}";
    }

    // Build material API ID
    public static string GetMatApiId(string matType, int tier, int enchant)
    {
        var enchantSuffix = enchant > 0 ? $"_LEVEL{enchant}@{enchant}" : "";
        return $"T{tier}_{matType}{enchantSuffix}";
    }

    // Build artifact API ID (tiered artifacts scale with item tier)
    public static string? GetArtifactApiId(GearItem item, int tier)
    {
        if (string.IsNullOrEmpty(item.ArtifactId)) return null;
        // Fixed-ID artifacts (QUESTITEM_, T4_SKILLBOOK_, etc.) don't scale
        if (item.ArtifactId.StartsWith("QUESTITEM_") || item.ArtifactId.StartsWith("T4_"))
            return item.ArtifactId;
        return $"T{tier}_{item.ArtifactId}";
    }

    public static string? GetArtifact2ApiId(GearItem item, int tier)
    {
        if (string.IsNullOrEmpty(item.Artifact2Id)) return null;
        return $"T{tier}_{item.Artifact2Id}";
    }

    public static readonly string[] Cities =
        ["Thetford", "Fort Sterling", "Bridgewatch", "Lymhurst", "Martlock", "Caerleon", "Brecilien", "Black Market"];

    // Preferred buy city per material type, based on Albion resource geography
    public static string GetPreferredCityForMat(string matId) =>
        matId.Contains("LEATHER")    ? "Martlock" :
        matId.Contains("METALBAR")   ? "Thetford" :
        matId.Contains("PLANKS")     ? "Fort Sterling" :
        matId.Contains("CLOTH")      ? "Lymhurst" :
        matId.Contains("STONEBLOCK") ? "Bridgewatch" :
        "Caerleon";

    public static readonly string[] Qualities =
        ["Normal", "Good", "Outstanding", "Excellent", "Masterpiece"];
}
