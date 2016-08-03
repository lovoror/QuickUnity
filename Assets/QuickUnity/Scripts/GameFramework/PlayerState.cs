namespace QuickUnity.GameFramework
{
    /// <summary>
    /// A PlayerState is created for every player on a server (or in a standalone game). PlayerStates
    /// are replicated to all clients, and contain network game relevant information about the
    /// player, such as playername, score, etc.
    /// </summary>
    /// <seealso cref="QuickUnity.GameFramework.Actor"/>
    public class PlayerState : Actor
    {
    }
}