public interface IWinnable
{

    void HandleSituation();

    void OnPressContinue();

    void AssignSceneSituation(string situation);

    string GetSceneSituation();

}