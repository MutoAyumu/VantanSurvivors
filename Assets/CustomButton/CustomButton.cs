using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(AudioSource))]
/// <summary>
/// �J�X�^���{�^���N���X
/// </summary>
public class CustomButton : MonoBehaviour, 
    IPointerClickHandler,
    IPointerDownHandler, 
    IPointerUpHandler, 
    IPointerEnterHandler, 
    IPointerExitHandler
{
    [Header("Sprite & Image")]
    [SerializeField, Tooltip("�|�C���^���I�u�W�F�N�g�������������ɍ����ւ���X�v���C�g")] 
    Sprite _onPointerDownSprite;

    [SerializeField, Tooltip("�|�C���^���I�u�W�F�N�g�ɏ�������ɕ\������C���[�W")] 
    Image _onPointerEnterImage;

    Image _buttonImage;
    Sprite _mainSprite;

    [Header("Audio")]
    [SerializeField, Tooltip("�|�C���^���I�u�W�F�N�g�ɏ�������ɏo������")] 
    AudioClip _onPointerEnterAudio;

    [SerializeField, Tooltip("�I�u�W�F�N�g��Ń|�C���^���������A����̃I�u�W�F�N�g��ŗ��������ɏo������")] 
    AudioClip _onPointerClickAudio;

    AudioSource _audioSource;

    [SerializeField] int _addSkillNumber = 1;

    /// <summary>
    /// �N���b�N�������ɂ�����������
    /// </summary>
    public event Action<int> OnClickCallback;

    private void Start()
    {
        _buttonImage = GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>();
        _mainSprite = _buttonImage.sprite;

        if(_onPointerEnterImage)
        _onPointerEnterImage.enabled = false;
        PlayerManager.Instance.SetSkillListener(this);
    }
    /// <summary>
    /// �N���b�N
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData) 
    {
        OnClickCallback?.Invoke(_addSkillNumber);
        Debug.Log("�N���b�N");

        if (!_onPointerClickAudio) return;

        _audioSource.PlayOneShot(_onPointerClickAudio);
    }
    /// <summary>
    /// �^�b�v�_�E��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData) 
    {
        if (!_onPointerDownSprite) return;

        _buttonImage.sprite = _onPointerDownSprite;
    }
    /// <summary>
    /// �^�b�v�A�b�v
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData) 
    {
        if (!_onPointerDownSprite) return;

        _buttonImage.sprite = _mainSprite;
    }
    public void OnPointerEnter(PointerEventData eventData) 
    {
        if (!_onPointerEnterImage) return;

        _onPointerEnterImage.enabled = true;

        if (!_onPointerEnterAudio) return;

        _audioSource.PlayOneShot(_onPointerEnterAudio);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_onPointerEnterImage) return;

        _onPointerEnterImage.enabled = false;
    }

}
