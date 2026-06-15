using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class UIMainGame : MonoBehaviour
{
    public UIDocument uid;
    private VisualElement _root;
    private Label _scoreText;
    private Button _gameQuitButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _root = uid.rootVisualElement;

        _scoreText = _root.Q<Label>("score-label");
        ScoreLabelUpdate(0000);

        _gameQuitButton = _root.Q<Button>("game-quit-button");
        //_gameQuitButton.clicked += Onclicked;
        _gameQuitButton.clicked += () =>
        // ラムダ式以下にクリック時の処理
        {

#if UNITY_EDITOR
            //editorの再生を止める(ビルドするときに止められる)
            EditorApplication.isPlaying = false;
#endif
            // ビルド後用
            Application.Quit();
        };
    }

    //public void Onclicked() {
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ScoreLabelUpdate(int num)
    {
        _scoreText.text = $"score:{num}";
    }
}
