

public class ToChaseActionNode : ActionNode
{
    public bool canReturnToChase = false;
    protected override void ExecuteAction()
    {
        canReturnToChase = true;
    }


    public void ResetCondition() => canReturnToChase = false;
}
