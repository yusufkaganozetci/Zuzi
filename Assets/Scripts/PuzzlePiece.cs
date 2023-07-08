using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    //[SerializeField] Transform[] puzzleBackgrounds;
    [SerializeField] int puzzlePieceIndex;
    private float snapDistance;
    private PuzzleManager puzzleManager;
    private GameHandler gameHandler;
    private bool isPlaced = false;
    private int lastPlacedIndex;


    private void Start()
    {
        puzzleManager = FindObjectOfType<PuzzleManager>();
        gameHandler = FindObjectOfType<GameHandler>();
        snapDistance = puzzleManager.GetSnapDistance();
    }

    private void OnMouseDrag()
    {
        if(gameHandler.GetTheGameIsStarted() && gameHandler.GetTheGameIsPlaying())
        {
            MovePuzzlePiece();
        }
    }

    private void OnMouseUp()
    {
        if (gameHandler.GetTheGameIsStarted() && gameHandler.GetTheGameIsPlaying())
        {
            PlacePuzzlePiece();
        }
    }

    private void MovePuzzlePiece()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;
        if (isPlaced)
        {
            puzzleManager.ChangeKeyValuePair(lastPlacedIndex, 0);
            isPlaced = false;
        }
    }

    private void PlacePuzzlePiece()
    {
        bool isPositionAssigned = false;
        Vector3 newPosition = Vector3.zero;
        float lastDistance = float.MaxValue;
        Transform[] puzzleBackgrounds = puzzleManager.GetPuzzleBackgrounds();
        int puzzleBackgroundIndex = 0;
        for (int i = 0; i < puzzleBackgrounds.Length; i++)
        {
            float currentDistance = Vector3.Distance(puzzleBackgrounds[i].position, transform.position);
            if (currentDistance <= snapDistance && currentDistance <= lastDistance)
            {
                puzzleBackgroundIndex = i;
                isPositionAssigned = true;
                lastDistance = currentDistance;
                newPosition = puzzleBackgrounds[i].position;
            }
        }
        if (isPositionAssigned)
        {
            isPlaced = true;
            lastPlacedIndex = puzzleBackgroundIndex;
            transform.position = newPosition;
            puzzleManager.ChangeKeyValuePair(puzzleBackgroundIndex, puzzlePieceIndex);
        }
    }

    
}
