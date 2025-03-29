using System;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Mangers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] CameraController cameraController;
        [SerializeField] PlayerController playerPrefab;

        PlayerController m_player;
        [Inject] NewInputSystem m_input;

        void OnEnable()
        {
            m_input.Enable();
        }


        void Start()
        {
            ReviveCharacter();
        }

        public PlayerController GetPlayer()
        {
            if (m_player == null || m_player.IsDestroyed()) return null;
            return m_player;
        }

        public void ReviveCharacter()
        {
            if (m_player == null)
            {
                m_player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity, transform);
                m_player.Install(m_input);
                cameraController.target = m_player.transform;
            }
        }

        public void ChangePlayerWeapon()
        {
            if (m_player == null) return;
            m_player.ChangeWeapon();
        }
    }
}