namespace EzEvade.Data
{
    public interface IChampionPlugin
    {
        string GetChampionName();
        void LoadSpecialSpell(SpellData spellData);
    }
}