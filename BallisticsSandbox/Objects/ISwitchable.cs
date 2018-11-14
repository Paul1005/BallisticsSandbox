namespace BallisticsSandbox.Objects
{
    /// <summary>
    /// Allows the switcher to change views easily.
    /// </summary>
    public interface ISwitchable
    {
        /// <summary>
        /// Utilize state takes in a user control, and will call the switcher
        /// object to change to that state.
        /// </summary>
        /// <param name="state"></param>
        void UtilizeState(object state);
    }
}