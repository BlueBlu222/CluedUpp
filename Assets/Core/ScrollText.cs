using UnityEngine;
//Moves the leaderboard scores up and down with arrow keys or buttons
namespace Scoring
{
    public class ScrollText : MonoBehaviour
    {
        public int buttonAmount = 5;
        ScrollText singleton;
        void Start()
        {
            if (singleton == null)
            {
                singleton = this;
            }
            else if (singleton != this)
            {
                Destroy(this);
            }
        }
        // Update is called once per frame
        void Update()
        {
            Move((int)Input.GetAxis("Vertical"));
        }
        void Move(int amount)
        {
            Highscore.addedPos = Mathf.Clamp(Highscore.addedPos + (amount), 0, Leaderboard.singleton.scoresDistance * -1 * (Leaderboard.scoreCount - 1));
        }
        public void Move(bool up)
        {
            Debug.Log("Clicked");
            Move(buttonAmount * ((up) ? 1 : -1));
        }
    }
}
