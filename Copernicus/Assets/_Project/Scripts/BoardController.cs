namespace _Project.Scripts
{
    using System.Collections.Generic;

    public class BoardController : SingletonBehaviour<BoardController>
    {
        private List<Block> _blocksOnBoard;

        public bool IsPositionValidForGroup(Group group)
        {
            return false;
        }

        public void PutGroupOnBoard(Group group)
        {
            //add blocks to board
        }
    }
}