namespace CrowdT
{
    public class PlayerInputView : CharacterInputView
    {
        protected override bool IsShouldBeAborted()
        {
            // if the first player turn was made during fade delay
            return FirstPlayerTurnOnLevelMadeTracker.IsMade;
        }
    }
}