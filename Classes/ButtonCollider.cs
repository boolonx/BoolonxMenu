using GorillaNetworking;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;
using static BoolonxMenu.Menu.Main;
using static BoolonxMenu.Settings;

namespace BoolonxMenu.Classes
{
	public class Button : MonoBehaviour
	{
		public string relatedText;

		public static float buttonCooldown = 0f;

		public bool EvacuateButton = false;

        public string RandomRoomGlyphs = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";


        public void OnTriggerEnter(Collider collider)
		{
			if (Time.time > buttonCooldown && collider == buttonCollider && menu != null)
			{
                buttonCooldown = Time.time + 0.2f;
                GorillaTagger.Instance.StartVibration(rightHanded, GorillaTagger.Instance.tagHapticStrength / 2f, GorillaTagger.Instance.tagHapticDuration / 2f);
                //VRRig.LocalRig.PlayHandTapLocal(67, rightHanded, 0.4f);
                float sldkfjds = (Random.Range(0, 2) == 0) ? 1.0f : 1.12246f;
                Plugin.menuSource.pitch = sldkfjds;
                Plugin.menuSource.PlayOneShot(Plugin.clickSound);
				if(!EvacuateButton)
				{
                    Toggle(this.relatedText);
                }
				else
				{
                    StartCoroutine(JoinLobbyInternal());
				}
            }
		}

		IEnumerator JoinLobbyInternal()
        {
            string randomGeneratedName = "BOOLONXMENU";

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < 12; i++)
            {
                int randomIndex = Random.Range(0, RandomRoomGlyphs.Length);
                stringBuilder.Append(RandomRoomGlyphs[randomIndex]);
            }

            randomGeneratedName = stringBuilder.ToString();

            PhotonNetwork.Disconnect();
            do
            {
                yield return new WaitForSeconds(1f);
            }
            while (PhotonNetwork.InRoom);
            
            string gamemodeCache = GorillaComputer.instance.currentGameMode.Value;
            PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(randomGeneratedName, JoinType.Solo);

            while (!PhotonNetwork.InRoom)
            {
                yield return new WaitForSeconds(1f);
            }
        }
	}
}
