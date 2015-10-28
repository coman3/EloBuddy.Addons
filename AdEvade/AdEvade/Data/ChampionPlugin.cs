namespace AdEvade.Data
{
    public interface IChampionPlugin
    {
        string GetChampionName();
        void LoadSpecialSpell(SpellData spellData);
    }
}