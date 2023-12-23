using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Microsoft.MixedReality.Toolkit;
using TMPro;

public class PursuitTracker : MonoBehaviour
{

    [SerializeField]
    private float InputZeitintervall;
    [SerializeField]
    private float InputSchritte;
    [SerializeField]
    private float InputMaxWinkel;
    [SerializeField]
    private float InputMaxLaengenabweichung;

    [Space(10)]

    [SerializeField]
    private GameObject MovingObjects;
    [SerializeField]
    private GameObject DebugInfo;

    [Space(10)]

    [SerializeField]
    private GameObject MovingObject1;
    [SerializeField]
    private GameObject MovingObject1Line;
    [SerializeField]
    private Vector3 MovingObject1Position0;
    [SerializeField]
    private Vector3 MovingObject1Position1;

    [Space(10)]

    [SerializeField]
    private GameObject MovingObject2;
    [SerializeField]
    private GameObject MovingObject2Line;
    [SerializeField]
    private Vector3 MovingObject2Position0;
    [SerializeField]
    private Vector3 MovingObject2Position1;

    [Space(10)]

    [SerializeField]
    private GameObject MovingObject3;
    [SerializeField]
    private GameObject MovingObject3Line;
    [SerializeField]
    private Vector3 MovingObject3Position0;
    [SerializeField]
    private Vector3 MovingObject3Position1;

    [Space(10)]

    [SerializeField]
    private GameObject MovingObject4;
    [SerializeField]
    private GameObject MovingObject4Line;
    [SerializeField]
    private Vector3 MovingObject4Position0;
    [SerializeField]
    private Vector3 MovingObject4Position1;

    [Space(10)]

    [SerializeField]
    private GameObject StartButton;
    [SerializeField]
    private float StartFixationZeit;
    [SerializeField]
    private GameObject StartFeedbackButton;
    [SerializeField]
    private float StartFeedbackZeit;

    [Space(10)]

    [SerializeField]
    private GameObject StopButton;
    [SerializeField]
    private float StopFixationZeit;
    [SerializeField]
    private GameObject StopFeedbackButton;
    [SerializeField]
    private float StopFeedbackZeit;

    [Space(10)]

    [SerializeField]
    private GameObject HUDStart;
    [SerializeField]
    private GameObject TutorialStartButton;
    [SerializeField]
    private GameObject TutorialStartButtonFeedback;
    [SerializeField]
    private GameObject TutorialStopButton;
    [SerializeField]
    private GameObject TutorialStopButtonFeedback;
    [SerializeField]
    private GameObject TutorialObjekt1;
    [SerializeField]
    private GameObject TutorialObjekt1Linie;
    [SerializeField]
    private GameObject TutorialObjekt2;
    [SerializeField]
    private GameObject TutorialObjekt2Linie;
    [SerializeField]
    private GameObject TutorialObjekt3;
    [SerializeField]
    private GameObject TutorialObjekt3Linie;
    [SerializeField]
    private GameObject TutorialObjekt4;
    [SerializeField]
    private GameObject TutorialObjekt4Linie;

    [Space(10)]

    [SerializeField]
    private AudioClip ClipDanke;
    [SerializeField]
    private AudioClip ClipTutorialStart;
    [SerializeField]
    private AudioClip ClipTutorialRichtung;
    [SerializeField]
    private AudioClip ClipTutorialStop;
    [SerializeField]
    private AudioClip ClipTutorialAnweisung;

    [Space(10)]

    [SerializeField]
    private AudioClip ClipStart;
    [SerializeField]
    private AudioClip ClipStop;
    [SerializeField]
    private AudioClip ClipVersuch1_01;
    [SerializeField]
    private AudioClip ClipVersuch1_02;
    [SerializeField]
    private AudioClip ClipVersuch1_03;
    [SerializeField]
    private AudioClip ClipVersuch1_04;
    [SerializeField]
    private AudioClip ClipVersuch2_01;
    [SerializeField]
    private AudioClip ClipVersuch2_02;
    [SerializeField]
    private AudioClip ClipVersuch2_03;
    [SerializeField]
    private AudioClip ClipVersuch2_04;
    [SerializeField]
    private AudioClip ClipVersuch2_A;
    [SerializeField]
    private AudioClip ClipVersuch2_B;
    [SerializeField]
    private AudioClip ClipVersuch2_C;
    [SerializeField]
    private AudioClip ClipVersuch2_D;
    [SerializeField]
    private AudioClip ClipVersuch3_01;
    [SerializeField]
    private AudioClip ClipVersuch3_02;
    [SerializeField]
    private AudioClip ClipVersuch3_03;
    [SerializeField]
    private AudioClip ClipVersuch3_04;
    [SerializeField]
    private AudioClip ClipVersuch3_05;
    [SerializeField]
    private AudioClip ClipVersuch3_06;
    [SerializeField]
    private AudioClip ClipVersuch3_07;
    [SerializeField]
    private AudioClip ClipVersuch3_08;
    [SerializeField]
    private AudioClip ClipVersuch3_09;
    [SerializeField]
    private AudioClip ClipVersuch3_10;
    [SerializeField]
    private AudioClip ClipVersuch3_11;
    [SerializeField]
    private AudioClip ClipVersuch3_12;
    [SerializeField]
    private AudioClip ClipVersuch4_01;
    [SerializeField]
    private AudioClip ClipVersuch4_02;
    [SerializeField]
    private AudioClip ClipVersuch4_03;
    [SerializeField]
    private AudioClip ClipVersuch4_04;

    //Werte zum Debuggen
    float Versuche = 0f;
    float ErfolgreicheVersuche = 0f;
    float KeineAuswahl = 0f;
    float Quote = 0f;

    //Phase der Interaktion
    float Phase = 0;

    //Eventlog
    public List<string[]> eventlog = new List<string[]>();

    //Events der Interaktion
    UnityEvent event_start;
    UnityEvent event_geradeaus;
    UnityEvent event_rueckwaerts;
    UnityEvent event_rechts;
    UnityEvent event_links;
    UnityEvent event_stop;

    //Eventlistener
    bool EventStartHappened;
    void EventStart(){
        EventStartHappened = true;
    }
    void ListenForEventStart(){
        if(EventStartHappened){
            StartCoroutine(EventStartWaitForCoroutine());
        }
    }
    IEnumerator EventStartWaitForCoroutine(){
        eventlog.Add(new string[] {"Aktion_Start",Time.time.ToString()});
        yield return new WaitForEndOfFrame();
        EventStartHappened = false;
    }

    bool EventGeradeausHappened;
    void EventGeradeaus(){
        EventGeradeausHappened = true;
    }
    void ListenForEventGeradeaus(){
        if(EventGeradeausHappened){
            StartCoroutine(EventGeradeausWaitForCoroutine());
        }
    }
    IEnumerator EventGeradeausWaitForCoroutine(){
        eventlog.Add(new string[] {"Aktion_Geradeaus",Time.time.ToString()});
        yield return new WaitForEndOfFrame();
        EventGeradeausHappened = false;
    }

    bool EventRueckwaertsHappened;
    void EventRueckwaerts(){
        EventRueckwaertsHappened = true;
    }
    void ListenForEventRueckwaerts(){
        if(EventRueckwaertsHappened){
            StartCoroutine(EventRueckwaertsWaitForCoroutine());
        }
    }
    IEnumerator EventRueckwaertsWaitForCoroutine(){
        eventlog.Add(new string[] {"Aktion_Rueckwaerts",Time.time.ToString()});
        yield return new WaitForEndOfFrame();
        EventRueckwaertsHappened = false;
    }

    bool EventRechtsHappened;
    void EventRechts(){
        EventRechtsHappened = true;
    }
    void ListenForEventRechts(){
        if(EventRechtsHappened){
            StartCoroutine(EventRechtsWaitForCoroutine());
        }
    }
    IEnumerator EventRechtsWaitForCoroutine(){
        eventlog.Add(new string[] {"Aktion_Rechts",Time.time.ToString()});
        yield return new WaitForEndOfFrame();
        EventRechtsHappened = false;
    }

    bool EventLinksHappened;
    void EventLinks(){
        EventLinksHappened = true;
    }
    void ListenForEventLinks(){
        if(EventLinksHappened){
            StartCoroutine(EventLinksWaitForCoroutine());
        }
    }
    IEnumerator EventLinksWaitForCoroutine(){
        eventlog.Add(new string[] {"Aktion_Links",Time.time.ToString()});
        yield return new WaitForEndOfFrame();
        EventLinksHappened = false;
    }

    bool EventStopHappened;
    void EventStop(){
        EventStopHappened = true;
    }
    void ListenForEventStop(){
        if(EventStopHappened){
            StartCoroutine(EventStopWaitForCoroutine());
        }
    }
    IEnumerator EventStopWaitForCoroutine(){
        eventlog.Add(new string[] {"Aktion_Stop",Time.time.ToString()});
        yield return new WaitForEndOfFrame();
        EventStopHappened = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Deklarierung der Events
        if (event_start == null){
            event_start = new UnityEvent();            
        }
        if (event_geradeaus == null){
            event_geradeaus = new UnityEvent();            
        }
        if (event_rueckwaerts == null){
            event_rueckwaerts = new UnityEvent();            
        }
        if (event_rechts == null){
            event_rechts = new UnityEvent();            
        }
        if (event_links == null){
            event_links = new UnityEvent();
        }            
        if (event_stop == null){
            event_stop = new UnityEvent();
        }            

        //Hinzufuegen der Listener
        event_start.AddListener(EventStart);
        event_geradeaus.AddListener(EventGeradeaus);
        event_rueckwaerts.AddListener(EventRueckwaerts);
        event_rechts.AddListener(EventRechts);
        event_links.AddListener(EventLinks);
        event_stop.AddListener(EventStop);
    }

    //Ist fuer die Bewegung der Objekte zustaendig und das erkennen von Folgebewegungen.
    IEnumerator MainCoroutine(float time, float steps)
    {

        while (Phase == 1) 
        {
            //Bewegte Objekte und ihre Bahnen werden aktiviert
            MovingObject1.SetActive(true);
            MovingObject1Line.SetActive(true);
            MovingObject2.SetActive(true);
            MovingObject2Line.SetActive(true);
            MovingObject3.SetActive(true);
            MovingObject3Line.SetActive(true);
            MovingObject4.SetActive(true);
            MovingObject4Line.SetActive(true);            

            //Position der Objekte wird auf Position 0 zurueck gesetzt
            MovingObject1.transform.localPosition = MovingObject1Position0;
            MovingObject2.transform.localPosition = MovingObject2Position0;
            MovingObject3.transform.localPosition = MovingObject3Position0;
            MovingObject4.transform.localPosition = MovingObject4Position0;

            //Wartet eine Sekunde um dem Nutzer Zeit zum Orientieren zu geben
            yield return new WaitForSeconds(1);

            //Stop Button wird aktiviert            
            if (!StopButton.activeSelf && Phase == 1)
            {
                StopButton.SetActive(true);
            }

            //Ermitteln der idealen Gaze Anfangsposition (funktioniert nur wenn die Anfangsposition alller Objekte gleich ist)
            Vector3 MovingObject1_IdealGazePosition0 = (MovingObject1.transform.position - CoreServices.InputSystem.EyeGazeProvider.GazeOrigin).normalized;
            Vector3 MovingObject2_IdealGazePosition0 = (MovingObject2.transform.position - CoreServices.InputSystem.EyeGazeProvider.GazeOrigin).normalized;
            Vector3 MovingObject3_IdealGazePosition0 = (MovingObject3.transform.position - CoreServices.InputSystem.EyeGazeProvider.GazeOrigin).normalized;
            Vector3 MovingObject4_IdealGazePosition0 = (MovingObject4.transform.position - CoreServices.InputSystem.EyeGazeProvider.GazeOrigin).normalized;
            
            //Ermitteln der realen Gaze Anfangsposition
            Vector3 RealGazePosition0 = CoreServices.InputSystem.EyeGazeProvider.GazeDirection.normalized;
            
            //Bewegen der Objekte von Anfangsposition zu Endposition in der gegebenen Zeit in den gegebenen Schritten (Bei zu vielen Schritten verlangsamt sich das Programm und die Zeit wird verzerrt!)        
            float step = (1/steps);
            while (MovingObject1.transform.localPosition != MovingObject1Position1)
            {
                MovingObject1.transform.localPosition = Vector3.Lerp(MovingObject1Position0, MovingObject1Position1, step);
                MovingObject2.transform.localPosition = Vector3.Lerp(MovingObject2Position0, MovingObject2Position1, step);
                MovingObject3.transform.localPosition = Vector3.Lerp(MovingObject3Position0, MovingObject3Position1, step);
                MovingObject4.transform.localPosition = Vector3.Lerp(MovingObject4Position0, MovingObject4Position1, step);

                //Rundung um Ungenauigkeiten des Float Typen zu vermeiden.
                step = (float)Math.Round(step+(1/steps),3);
                yield return new WaitForSeconds(time/steps);  
            }

            //Ermitteln der idealen Gaze Endposition
            Vector3 MovingObject1_IdealGazePosition1 = (MovingObject1.transform.position - CoreServices.InputSystem.EyeGazeProvider.GazeOrigin).normalized;
            Vector3 MovingObject2_IdealGazePosition1 = (MovingObject2.transform.position - CoreServices.InputSystem.EyeGazeProvider.GazeOrigin).normalized;
            Vector3 MovingObject3_IdealGazePosition1 = (MovingObject3.transform.position - CoreServices.InputSystem.EyeGazeProvider.GazeOrigin).normalized;
            Vector3 MovingObject4_IdealGazePosition1 = (MovingObject4.transform.position - CoreServices.InputSystem.EyeGazeProvider.GazeOrigin).normalized;

            //Ermitteln der realen Gaze Endposition
            Vector3 RealGazePosition1 = CoreServices.InputSystem.EyeGazeProvider.GazeDirection.normalized;

            //Berechnen der idealen Gaze Richtung
            Vector3 MovingObject1_IdealGazeDirection = MovingObject1_IdealGazePosition1 - MovingObject1_IdealGazePosition0;
            Vector3 MovingObject2_IdealGazeDirection = MovingObject2_IdealGazePosition1 - MovingObject2_IdealGazePosition0;
            Vector3 MovingObject3_IdealGazeDirection = MovingObject3_IdealGazePosition1 - MovingObject3_IdealGazePosition0;
            Vector3 MovingObject4_IdealGazeDirection = MovingObject4_IdealGazePosition1 - MovingObject4_IdealGazePosition0;

            //Berechnen der realen Gaze Richtung
            Vector3 RealGazeDirection = RealGazePosition1 - RealGazePosition0;

            //Berechnen des Winkels zwischen der realen Gaze Richtung und der jeweiligen idealen Gaze Richtung
            float MovingObject1_Angle = Vector3.Angle(RealGazeDirection, MovingObject1_IdealGazeDirection);
            float MovingObject2_Angle = Vector3.Angle(RealGazeDirection, MovingObject2_IdealGazeDirection);
            float MovingObject3_Angle = Vector3.Angle(RealGazeDirection, MovingObject3_IdealGazeDirection);
            float MovingObject4_Angle = Vector3.Angle(RealGazeDirection, MovingObject4_IdealGazeDirection);

            //Berechnen des kleinsten Winkels
            float SelectedObject_Angle = Mathf.Min(Mathf.Min(MovingObject1_Angle, MovingObject2_Angle), Mathf.Min(MovingObject3_Angle, MovingObject4_Angle));

            //Berechnen der maximalen Laengenabweichung basierend auf Input
            float MaxLaengenabweichung = InputMaxLaengenabweichung;

            //Ermitteln des ausgewaehlten Objekts anhand des kleinsten Winkels. Auswahl, wenn dieser unter der maximalen Winkelabweichung liegt.
            int result = 0;
            if (SelectedObject_Angle < InputMaxWinkel)
            {
                if (MovingObject1_Angle == SelectedObject_Angle) 
                {
                    //Auswahl, wenn maximale Laengenabweichung nicht ueberschritten wird.
                    if (RealGazeDirection.magnitude/MovingObject1_IdealGazeDirection.magnitude > 1-MaxLaengenabweichung && RealGazeDirection.magnitude/MovingObject1_IdealGazeDirection.magnitude < 1+MaxLaengenabweichung)
                    {
                        //Objekt 1 wurde ausgewaehlt
                        result = 1;
                        event_geradeaus.Invoke();
                        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);

                    }  
                }
                if (MovingObject2_Angle == SelectedObject_Angle) 
                {
                    //Auswahl, wenn maximale Laengenabweichung nicht ueberschritten wird.
                    if (RealGazeDirection.magnitude/MovingObject2_IdealGazeDirection.magnitude > 1-MaxLaengenabweichung && RealGazeDirection.magnitude/MovingObject2_IdealGazeDirection.magnitude < 1+MaxLaengenabweichung)
                    {
                        //Objekt 2 wurde ausgewaehlt
                        result = 2;
                        event_rueckwaerts.Invoke();
                        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    }
                }
                if (MovingObject3_Angle == SelectedObject_Angle) 
                {
                    //Auswahl, wenn maximale Laengenabweichung nicht ueberschritten wird.
                    if (RealGazeDirection.magnitude/MovingObject3_IdealGazeDirection.magnitude > 1-MaxLaengenabweichung && RealGazeDirection.magnitude/MovingObject3_IdealGazeDirection.magnitude < 1+MaxLaengenabweichung)
                    {
                        //Objekt 3 wurde ausgewaehlt
                        result = 3;
                        event_rechts.Invoke();
                        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);

                    }
                }
                if (MovingObject4_Angle == SelectedObject_Angle) 
                {
                    //Auswahl, wenn maximale Laengenabweichung nicht ueberschritten wird.
                    if (RealGazeDirection.magnitude/MovingObject4_IdealGazeDirection.magnitude > 1-MaxLaengenabweichung && RealGazeDirection.magnitude/MovingObject4_IdealGazeDirection.magnitude < 1+MaxLaengenabweichung)
                    {
                        //Objekt 4 wurde ausgewaehlt
                        result = 4;
                        event_links.Invoke();
                        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                    }
                }
                //Wenn bereits Stop erfolgte, keine Auswahl
                if(Phase == 2){
                    result = 0;
                }
                if(result == 0)
                {
                    MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                }
            }

            yield return new WaitForSeconds(0.5f);


        }

    }

    //Ruft die MainCoroutine auf, da Coroutinen nicht direkt durch Buttons aufgerufen werden koennen.
    public void StartMainCoroutine()
    {        
        StartCoroutine(MainCoroutine(InputZeitintervall, InputSchritte));
    }

    //Spielt das Tutorial ab und startet Versuch 1.
    IEnumerator Tutorial()
    {
        AudioSource audio = GetComponent<AudioSource>();

        //Abspielen des Clips "Danke"
        audio.clip = ClipDanke;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        yield return new WaitForSeconds(0.5f);
        TutorialStartButton.SetActive(true);

        //Abspielen des Clips "Tutorial_Start"
        audio.clip = ClipTutorialStart;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Simulieren der Start Button Auswahl
        TutorialStartButtonFeedback.SetActive(true);
        TutorialStartButtonFeedback.transform.localScale = new Vector3(0.2f,0.1f,0.2f);
        yield return new WaitForSeconds(0.085f);
        TutorialStartButtonFeedback.transform.localScale = new Vector3(0.4f,0.1f,0.4f);
        yield return new WaitForSeconds(0.085f);
        TutorialStartButtonFeedback.transform.localScale = new Vector3(0.6f,0.1f,0.6f);
        yield return new WaitForSeconds(0.085f);
        TutorialStartButtonFeedback.transform.localScale = new Vector3(0.8f,0.1f,0.8f);
        yield return new WaitForSeconds(0.085f);
        TutorialStartButtonFeedback.transform.localScale = new Vector3(1.0f,0.1f,1.0f);
        yield return new WaitForSeconds(0.085f);
        TutorialStartButtonFeedback.transform.localScale = new Vector3(1.2f,0.1f,1.2f);
        yield return new WaitForSeconds(0.085f);
        TutorialStartButtonFeedback.transform.localScale = new Vector3(1.4f,0.1f,1.4f);
        yield return new WaitForSeconds(0.085f);
        TutorialStartButtonFeedback.transform.localScale = new Vector3(1.6f,0.1f,1.6f);
        yield return new WaitForSeconds(0.085f);
        TutorialStartButtonFeedback.transform.localScale = new Vector3(1.8f,0.1f,1.8f);
        yield return new WaitForSeconds(0.085f);
        TutorialStartButtonFeedback.transform.localScale = new Vector3(2.0f,0.1f,2.0f);
        yield return new WaitForSeconds(0.085f);

        TutorialStartButton.SetActive(false);
        TutorialStartButtonFeedback.SetActive(false);
        TutorialStopButton.SetActive(true);
        
        TutorialObjekt1.SetActive(true);
        TutorialObjekt1Linie.SetActive(true);
        TutorialObjekt2.SetActive(true);
        TutorialObjekt2Linie.SetActive(true);
        TutorialObjekt3.SetActive(true);
        TutorialObjekt3Linie.SetActive(true);
        TutorialObjekt4.SetActive(true);
        TutorialObjekt4Linie.SetActive(true);

        //Abspielen des Clips "Tutorial_Richtung"
        audio.clip = ClipTutorialRichtung;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Simulieren der Richtungsauswahl
        yield return new WaitForSeconds(0.5f);
        float i = 0f;
        while (i<1)
        {
            TutorialObjekt1.transform.localPosition = Vector3.Lerp(MovingObject1Position0, MovingObject1Position1, i);
            TutorialObjekt2.transform.localPosition = Vector3.Lerp(MovingObject2Position0, MovingObject2Position1, i);
            TutorialObjekt3.transform.localPosition = Vector3.Lerp(MovingObject3Position0, MovingObject3Position1, i);
            TutorialObjekt4.transform.localPosition = Vector3.Lerp(MovingObject4Position0, MovingObject4Position1, i);
            yield return new WaitForSeconds(0.035f);
            i = i + 0.05f;
        }
        TutorialObjekt1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        yield return new WaitForSeconds(1f);
        i = 0;
        while (i<1)
        {
            TutorialObjekt1.transform.localPosition = Vector3.Lerp(MovingObject1Position0, MovingObject1Position1, i);
            TutorialObjekt2.transform.localPosition = Vector3.Lerp(MovingObject2Position0, MovingObject2Position1, i);
            TutorialObjekt3.transform.localPosition = Vector3.Lerp(MovingObject3Position0, MovingObject3Position1, i);
            TutorialObjekt4.transform.localPosition = Vector3.Lerp(MovingObject4Position0, MovingObject4Position1, i);
            yield return new WaitForSeconds(0.035f);
            i = i + 0.05f;
        }
        TutorialObjekt1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        TutorialObjekt2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Tutorial_Stop"
        audio.clip = ClipTutorialStop;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Simulieren der Stop Button Auswahl
        TutorialStopButtonFeedback.SetActive(true);
        TutorialStopButtonFeedback.transform.localScale = new Vector3(0.2f,0.1f,0.2f);
        yield return new WaitForSeconds(0.065f);
        TutorialStopButtonFeedback.transform.localScale = new Vector3(0.4f,0.1f,0.4f);
        yield return new WaitForSeconds(0.065f);
        TutorialStopButtonFeedback.transform.localScale = new Vector3(0.6f,0.1f,0.6f);
        yield return new WaitForSeconds(0.065f);
        TutorialStopButtonFeedback.transform.localScale = new Vector3(0.8f,0.1f,0.8f);
        yield return new WaitForSeconds(0.065f);
        TutorialStopButtonFeedback.transform.localScale = new Vector3(1.0f,0.1f,1.0f);
        yield return new WaitForSeconds(0.065f);
        TutorialStopButtonFeedback.transform.localScale = new Vector3(1.2f,0.1f,1.2f);
        yield return new WaitForSeconds(0.065f);
        TutorialStopButtonFeedback.transform.localScale = new Vector3(1.4f,0.1f,1.4f);
        yield return new WaitForSeconds(0.065f);
        TutorialStopButtonFeedback.transform.localScale = new Vector3(1.6f,0.1f,1.6f);
        yield return new WaitForSeconds(0.065f);
        TutorialStopButtonFeedback.transform.localScale = new Vector3(1.8f,0.1f,1.8f);
        yield return new WaitForSeconds(0.065f);
        TutorialStopButtonFeedback.transform.localScale = new Vector3(2.0f,0.1f,2.0f);
        yield return new WaitForSeconds(0.065f);

        TutorialStopButton.SetActive(false);
        TutorialStopButtonFeedback.SetActive(false);

        TutorialObjekt1.SetActive(false);
        TutorialObjekt1Linie.SetActive(false);
        TutorialObjekt2.SetActive(false);
        TutorialObjekt2Linie.SetActive(false);
        TutorialObjekt3.SetActive(false);
        TutorialObjekt3Linie.SetActive(false);
        TutorialObjekt4.SetActive(false);
        TutorialObjekt4Linie.SetActive(false);
        yield return new WaitForSeconds(1f);  

        //Abspielen des Clips "Tutorial_Anweisung"
        audio.clip = ClipTutorialAnweisung;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);   
        yield return new WaitForSeconds(1f);   

        //Starte Versuch 1
        yield return Versuch1();

    }

    //Versuch1: Einfacher Fahrvorgang ohne Richtungsaenderung
    IEnumerator Versuch1()
    {
        AudioSource audio = GetComponent<AudioSource>();

        //-----------------------------Versuch1_01-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangohneRichtungsaenderung_geradeaus"
        audio.clip = ClipVersuch1_01;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch01_01_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventGeradeausHappened);
        EventGeradeausHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch01_01_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch1_02-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangohneRichtungsaenderung_rueckwaerts"
        audio.clip = ClipVersuch1_02;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch01_02_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventRueckwaertsHappened);
        EventRueckwaertsHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch01_02_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch1_03-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangohneRichtungsaenderung_links"
        audio.clip = ClipVersuch1_03;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);        
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch01_03_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventLinksHappened);
        EventLinksHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch01_03_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch1_04-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangohneRichtungsaenderung_rechts"
        audio.clip = ClipVersuch1_04;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch01_04_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventRechtsHappened);
        EventRechtsHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch01_04_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        StartCoroutine(Versuch2());
    }

    //Versuch2: Einfacher Fahrvorgang mit Richtungsaenderung
    IEnumerator Versuch2()
    {
        AudioSource audio = GetComponent<AudioSource>();

        //-----------------------------Versuch2_01B-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitRichtungsaenderung_geradeaus"
        audio.clip = ClipVersuch2_01;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch02_01B_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventGeradeausHappened);
        EventGeradeausHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Richtungsaenderung_rueckwaerts"
        audio.clip = ClipVersuch2_B;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white); 

        eventlog.Add(new string[] {"Versuch02_01B_Richtungsaenderung",Time.time.ToString()});

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventRueckwaertsHappened);
        EventRueckwaertsHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch02_01B_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch2_01C-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitRichtungsaenderung_geradeaus"
        audio.clip = ClipVersuch2_01;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch02_01C_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventGeradeausHappened);
        EventGeradeausHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Richtungsaenderung_rechts"
        audio.clip = ClipVersuch2_C;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white); 
 
        eventlog.Add(new string[] {"Versuch02_01C_Richtungsaenderung",Time.time.ToString()});

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventRechtsHappened);
        EventRechtsHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch02_01C_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch2_01D-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitRichtungsaenderung_geradeaus"
        audio.clip = ClipVersuch2_01;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch02_01D_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventGeradeausHappened);
        EventGeradeausHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Richtungsaenderung_links"
        audio.clip = ClipVersuch2_D;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue); 

        eventlog.Add(new string[] {"Versuch02_01D_Richtungsaenderung",Time.time.ToString()});

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventLinksHappened);
        EventLinksHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch02_01D_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch2_02A-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitRichtungsaenderung_rueckwaerts"
        audio.clip = ClipVersuch2_02;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch02_02A_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventRueckwaertsHappened);
        EventRueckwaertsHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Richtungsaenderung_geradeaus"
        audio.clip = ClipVersuch2_A;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white); 

        eventlog.Add(new string[] {"Versuch02_02A_Richtungsaenderung",Time.time.ToString()});

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventGeradeausHappened);
        EventGeradeausHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch02_02A_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch2_02C-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitRichtungsaenderung_rueckwaerts"
        audio.clip = ClipVersuch2_02;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch02_02C_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventRueckwaertsHappened);
        EventRueckwaertsHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Richtungsaenderung_rechts"
        audio.clip = ClipVersuch2_C;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white); 

        eventlog.Add(new string[] {"Versuch02_02C_Richtungsaenderung",Time.time.ToString()});

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventRechtsHappened);
        EventRechtsHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch02_02C_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch2_02D-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitRichtungsaenderung_rueckwaerts"
        audio.clip = ClipVersuch2_02;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch02_02C_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventRueckwaertsHappened);
        EventRueckwaertsHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Richtungsaenderung_links"
        audio.clip = ClipVersuch2_D;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue); 

        eventlog.Add(new string[] {"Versuch02_02D_Richtungsaenderung",Time.time.ToString()});

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventLinksHappened);
        EventLinksHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch02_02D_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch2_03A-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitRichtungsaenderung_rechts"
        audio.clip = ClipVersuch2_03;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch02_03A_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventRechtsHappened);
        EventRechtsHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Richtungsaenderung_geradeaus"
        audio.clip = ClipVersuch2_A;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white); 

        eventlog.Add(new string[] {"Versuch02_03A_Richtungsaenderung",Time.time.ToString()});

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventGeradeausHappened);
        EventGeradeausHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch02_03A_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch2_03B-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitRichtungsaenderung_rechts"
        audio.clip = ClipVersuch2_03;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch02_03B_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventRechtsHappened);
        EventRechtsHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Richtungsaenderung_rueckwaerts"
        audio.clip = ClipVersuch2_B;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white); 

        eventlog.Add(new string[] {"Versuch02_03B_Richtungsaenderung",Time.time.ToString()});

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventRueckwaertsHappened);
        EventRueckwaertsHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch02_03B_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch2_03D-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitRichtungsaenderung_rechts"
        audio.clip = ClipVersuch2_03;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch02_03D_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventRechtsHappened);
        EventRechtsHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Richtungsaenderung_links"
        audio.clip = ClipVersuch2_D;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue); 

        eventlog.Add(new string[] {"Versuch02_03D_Richtungsaenderung",Time.time.ToString()});

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventLinksHappened);
        EventLinksHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch02_03D_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch2_04A-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitRichtungsaenderung_links"
        audio.clip = ClipVersuch2_04;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch02_04A_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventLinksHappened);
        EventLinksHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Richtungsaenderung_geradeaus"
        audio.clip = ClipVersuch2_A;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white); 

        eventlog.Add(new string[] {"Versuch02_04A_Richtungsaenderung",Time.time.ToString()});

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventGeradeausHappened);
        EventGeradeausHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch02_04A_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch2_04B-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitRichtungsaenderung_links"
        audio.clip = ClipVersuch2_04;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch02_04B_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventLinksHappened);
        EventLinksHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Richtungsaenderung_rueckwaerts"
        audio.clip = ClipVersuch2_B;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white); 

        eventlog.Add(new string[] {"Versuch02_04B_Richtungsaenderung",Time.time.ToString()});

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventRueckwaertsHappened);
        EventRueckwaertsHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch02_04B_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch2_04C-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitRichtungsaenderung_links"
        audio.clip = ClipVersuch2_04;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch02_04C_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventLinksHappened);
        EventLinksHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Richtungsaenderung_rechts"
        audio.clip = ClipVersuch2_C;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white); 

        eventlog.Add(new string[] {"Versuch02_04C_Richtungsaenderung",Time.time.ToString()});

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventLinksHappened);
        EventLinksHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch02_04C_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        StartCoroutine(Versuch3());
    }

    //Versuch3: Einfacher Fahrvorgang mit Korrektur
    IEnumerator Versuch3()
    {
        AudioSource audio = GetComponent<AudioSource>();

        //-----------------------------Versuch3_01-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitKorrektur_geradeaus_rueckwaerts"
        audio.clip = ClipVersuch3_01;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch03_01_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventGeradeausHappened);
        EventGeradeausHappened = false;

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white); 

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventRueckwaertsHappened);
        EventRueckwaertsHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch03_01_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch3_02-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitKorrektur_geradeaus_rechts"
        audio.clip = ClipVersuch3_02;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch03_02_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventGeradeausHappened);
        EventGeradeausHappened = false;

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white); 

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventRechtsHappened);
        EventRechtsHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch03_02_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch3_03-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitKorrektur_geradeaus_links"
        audio.clip = ClipVersuch3_03;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch03_03_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventGeradeausHappened);
        EventGeradeausHappened = false;

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue); 

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventLinksHappened);
        EventLinksHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch03_03_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch3_04-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitKorrektur_rueckwaerts_geradeaus"
        audio.clip = ClipVersuch3_04;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch03_04_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventRueckwaertsHappened);
        EventRueckwaertsHappened = false;

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white); 

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventGeradeausHappened);
        EventGeradeausHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch03_04_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch3_05-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitKorrektur_rueckwaerts_rechts"
        audio.clip = ClipVersuch3_05;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch03_05_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventRueckwaertsHappened);
        EventRueckwaertsHappened = false;

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white); 

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventRechtsHappened);
        EventRechtsHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch03_05_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch3_06-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitKorrektur_rueckwaerts_links"
        audio.clip = ClipVersuch3_06;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch03_06_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventRueckwaertsHappened);
        EventRueckwaertsHappened = false;

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue); 

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventLinksHappened);
        EventLinksHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch03_06_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch3_07-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitKorrektur_rechts_geradeaus"
        audio.clip = ClipVersuch3_07;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch03_07_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventRechtsHappened);
        EventRechtsHappened = false;

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white); 

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventGeradeausHappened);
        EventGeradeausHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch03_07_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch3_08-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitKorrektur_rechts_rueckwaerts"
        audio.clip = ClipVersuch3_08;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch03_08_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventRechtsHappened);
        EventRechtsHappened = false;

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white); 

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventRueckwaertsHappened);
        EventRueckwaertsHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch03_08_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch3_09-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitKorrektur_rechts_links"
        audio.clip = ClipVersuch3_09;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch03_09_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventRechtsHappened);
        EventRechtsHappened = false;

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue); 

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventLinksHappened);
        EventLinksHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch03_09_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch3_10-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitKorrektur_links_geradeaus"
        audio.clip = ClipVersuch3_10;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch03_10_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventLinksHappened);
        EventLinksHappened = false;

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white); 

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventGeradeausHappened);
        EventGeradeausHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch03_10_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch3_11-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitKorrektur_links_rueckwaerts"
        audio.clip = ClipVersuch3_11;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch03_11_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventLinksHappened);
        EventLinksHappened = false;

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white); 

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventRueckwaertsHappened);
        EventRueckwaertsHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch03_11_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch3_12-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitKorrektur_links_geradeaus"
        audio.clip = ClipVersuch3_12;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch03_12_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventLinksHappened);
        EventLinksHappened = false;

        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white); 

        //Warte auf Richtungsaenderung
        yield return new WaitUntil(() => EventRechtsHappened);
        EventRechtsHappened = false;

        //Warte 2 Sekunden
        yield return new WaitForSeconds(2f);

        //Abspielen des Clips "Stop"
        audio.clip = ClipStop;
        audio.Play(); 
        eventlog.Add(new string[] {"Versuch03_12_Stop",Time.time.ToString()});

        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        StartCoroutine(Versuch4());
    }

    //Versuch4: Einfacher Fahrvorgang mit direktem Stop
    IEnumerator Versuch4()
    {
        AudioSource audio = GetComponent<AudioSource>();

        //-----------------------------Versuch4_01-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitdirektemStop_geradeaus"
        audio.clip = ClipVersuch4_01;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch04_01_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventGeradeausHappened);
        EventGeradeausHappened = false;
        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch4_02-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitdirektemStop_rueckwaerts"
        audio.clip = ClipVersuch4_02;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch04_02_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventRueckwaertsHappened);
        EventRueckwaertsHappened = false;
        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch4_03-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitdirektemStop_rechts"
        audio.clip = ClipVersuch4_03;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch04_03_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventRechtsHappened);
        EventRechtsHappened = false;
        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        //-----------------------------Versuch4_04-----------------------------        
        //Abspielen des Clips "EinfacherfahrvorgangmitdirektemStop_links"
        audio.clip = ClipVersuch4_04;
        audio.Play(); 
        yield return new WaitForSeconds(audio.clip.length);

        //Activieren des Interfaces
        MovingObjects.SetActive(true);
        MovingObject1.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject2.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject3.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MovingObject4.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        yield return new WaitForSeconds(1f);

        //Abspielen des Clips "Start"
        audio.clip = ClipStart;
        audio.Play();
        eventlog.Add(new string[] {"Versuch04_04_Start",Time.time.ToString()});

        //Warte auf Start und Richtungsauswahl
        yield return new WaitUntil(() => EventStartHappened);
        EventStartHappened = false;
        yield return new WaitUntil(() => EventLinksHappened);
        EventLinksHappened = false;
        yield return new WaitUntil(() => EventStopHappened);
        EventStopHappened = false;

        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------

        // foreach (var element in eventlog){
        //     print(element[0]+": "+element[1]);
        // }
    }

    public void StartVersuchsablauf()
    {
        HUDStart.SetActive(false);
        StartCoroutine(Tutorial());                                      
    }

    


    //Wird durch Input_RotationX und Input_RotationY aufgerufen. Aendert die Rotation der bewegten Objekte.
    // public void ChangeMovingObjectsAngle()
    // {
    //     Quaternion target = Quaternion.Euler(270+float.Parse(InputRotationX.GetComponent<TMP_InputField>().text), 0, float.Parse(InputRotationZ.GetComponent<TMP_InputField>().text));
    //     MovingObjects.transform.rotation = target;        
    // }

    //Setzt das Debug Info Textfeld im Conroll Pannel und die zugehoerigen Werte zurueck.
    public void ClearDebugInfo()
    {
        Versuche = 0f;
        ErfolgreicheVersuche = 0f;
        KeineAuswahl = 0f;
        Quote = 0f;
        TextMeshPro DebugInfo_TMP = DebugInfo.GetComponent<TextMeshPro>();
        DebugInfo_TMP.text = "Debug Info \n\nVersuche: " + Versuche + "\nErfolgreich: " + ErfolgreicheVersuche + "\nKeine Auswahl: " + KeineAuswahl +"\n\nQuote: " + Quote + "%";
    }

    float StartButtonFixationZeitpunkt0 = 0f;
    bool StartButtonFeedbackActive = false;
    //Prueft nach Fixationen auf den Start Button und gibt visuelles Feedback. Bei erfolgreicher Fixation wird die Bewegung der Objekte (MainCoroutine) gestartet.
    public void CheckStartButton()
    {
        //Anfang der Fixation
        if ((CoreServices.InputSystem.EyeGazeProvider.GazeTarget == StartButton || CoreServices.InputSystem.EyeGazeProvider.GazeTarget == StartFeedbackButton) && StartButtonFixationZeitpunkt0 == 0)
        {
            StartButtonFixationZeitpunkt0 = Time.time;
        }
        //Abbruch der Fixation
        if (CoreServices.InputSystem.EyeGazeProvider.GazeTarget != StartButton && CoreServices.InputSystem.EyeGazeProvider.GazeTarget != StartFeedbackButton && StartButtonFixationZeitpunkt0 != 0)
        {
            StartFeedbackButton.SetActive(false);
            StartButtonFixationZeitpunkt0 = 0;            
        }
        //Visuelles Feedback
        if ((CoreServices.InputSystem.EyeGazeProvider.GazeTarget == StartButton || CoreServices.InputSystem.EyeGazeProvider.GazeTarget == StartFeedbackButton) && Time.time-StartButtonFixationZeitpunkt0 > StartFeedbackZeit && Time.time-StartButtonFixationZeitpunkt0 < StartFixationZeit && StartButtonFeedbackActive == false)
        {
            StartFeedbackButton.SetActive(true);
            float StartFeedbackIntervall = StartFixationZeit - StartFeedbackZeit;

            if(Time.time-StartButtonFixationZeitpunkt0-StartFeedbackZeit < StartFeedbackIntervall*0.1f)
            {
                StartFeedbackButton.transform.localScale = new Vector3(0.2f,0.1f,0.2f);
            }      
            if(Time.time-StartButtonFixationZeitpunkt0-StartFeedbackZeit > StartFeedbackIntervall*0.1f && Time.time-StartButtonFixationZeitpunkt0-StartFeedbackZeit < StartFeedbackIntervall*0.2f)
            {
                StartFeedbackButton.transform.localScale = new Vector3(0.4f,0.1f,0.4f);
            }      
            if(Time.time-StartButtonFixationZeitpunkt0-StartFeedbackZeit > StartFeedbackIntervall*0.2f && Time.time-StartButtonFixationZeitpunkt0-StartFeedbackZeit < StartFeedbackIntervall*0.3f)
            {
                StartFeedbackButton.transform.localScale = new Vector3(0.6f,0.1f,0.6f);
            }      
            if(Time.time-StartButtonFixationZeitpunkt0-StartFeedbackZeit > StartFeedbackIntervall*0.3f && Time.time-StartButtonFixationZeitpunkt0-StartFeedbackZeit < StartFeedbackIntervall*0.4f)
            {
                StartFeedbackButton.transform.localScale = new Vector3(0.8f,0.1f,0.8f);
            }      
            if(Time.time-StartButtonFixationZeitpunkt0-StartFeedbackZeit > StartFeedbackIntervall*0.4f && Time.time-StartButtonFixationZeitpunkt0-StartFeedbackZeit < StartFeedbackIntervall*0.5f)
            {
                StartFeedbackButton.transform.localScale = new Vector3(1f,0.1f,1f);
            }      
            if(Time.time-StartButtonFixationZeitpunkt0-StartFeedbackZeit > StartFeedbackIntervall*0.5f && Time.time-StartButtonFixationZeitpunkt0-StartFeedbackZeit < StartFeedbackIntervall*0.6f)
            {
                StartFeedbackButton.transform.localScale = new Vector3(1.2f,0.1f,1.2f);
            }      
            if(Time.time-StartButtonFixationZeitpunkt0-StartFeedbackZeit > StartFeedbackIntervall*0.6f && Time.time-StartButtonFixationZeitpunkt0-StartFeedbackZeit < StartFeedbackIntervall*0.7f)
            {
                StartFeedbackButton.transform.localScale = new Vector3(1.4f,0.1f,1.4f);
            }      
            if(Time.time-StartButtonFixationZeitpunkt0-StartFeedbackZeit > StartFeedbackIntervall*0.7f && Time.time-StartButtonFixationZeitpunkt0-StartFeedbackZeit < StartFeedbackIntervall*0.8f)
            {
                StartFeedbackButton.transform.localScale = new Vector3(1.6f,0.1f,1.6f);
            }      
            if(Time.time-StartButtonFixationZeitpunkt0-StartFeedbackZeit > StartFeedbackIntervall*0.8f && Time.time-StartButtonFixationZeitpunkt0-StartFeedbackZeit < StartFeedbackIntervall*0.9f)
            {
                StartFeedbackButton.transform.localScale = new Vector3(1.8f,0.1f,1.8f);
            }      
            if(Time.time-StartButtonFixationZeitpunkt0-StartFeedbackZeit > StartFeedbackIntervall*0.9f)
            {
                StartFeedbackButton.transform.localScale = new Vector3(2f,0.1f,2f);

            }      
        }
        //Auswahl durch Fixation
        if ((CoreServices.InputSystem.EyeGazeProvider.GazeTarget == StartButton || CoreServices.InputSystem.EyeGazeProvider.GazeTarget == StartFeedbackButton) && Time.time-StartButtonFixationZeitpunkt0 > StartFixationZeit)
        {
            StartButtonFixationZeitpunkt0 = 0;
            event_start.Invoke();

            //Start Phase 1
            Phase = 1;
            StartMainCoroutine();
            StartButton.SetActive(false);
            StartFeedbackButton.SetActive(false);
        }

    }

    float StopButtonFixationZeitpunkt0 = 0f;
    bool StopButtonFeedbackActive = false;
    //Prueft nach Fixationen auf den Stop Button und gibt visuelles Feedback. Bei erfolgreicher Fixation wird die Bewegung der Objekte (MainCoroutine) gestopt.
    public void CheckStopButton()
    {
        //Anfang der Fixation
        if ((CoreServices.InputSystem.EyeGazeProvider.GazeTarget == StopButton || CoreServices.InputSystem.EyeGazeProvider.GazeTarget == StopFeedbackButton) && StopButtonFixationZeitpunkt0 == 0)
        {
            StopButtonFixationZeitpunkt0 = Time.time;
        }
        //Abbruch der Fixation
        if (CoreServices.InputSystem.EyeGazeProvider.GazeTarget != StopButton && CoreServices.InputSystem.EyeGazeProvider.GazeTarget != StopFeedbackButton && StopButtonFixationZeitpunkt0 != 0)
        {
            StopFeedbackButton.SetActive(false);
            StopButtonFixationZeitpunkt0 = 0;            
        }
        //Visuelles Feedback
        if ((CoreServices.InputSystem.EyeGazeProvider.GazeTarget == StopButton || CoreServices.InputSystem.EyeGazeProvider.GazeTarget == StopFeedbackButton) && Time.time-StopButtonFixationZeitpunkt0 > StopFeedbackZeit && Time.time-StopButtonFixationZeitpunkt0 < StopFixationZeit && StopButtonFeedbackActive == false)
        {
            StopFeedbackButton.SetActive(true);
            float StopFeedbackIntervall = StopFixationZeit - StopFeedbackZeit;

            if(Time.time-StopButtonFixationZeitpunkt0-StopFeedbackZeit < StopFeedbackIntervall*0.1f)
            {
                StopFeedbackButton.transform.localScale = new Vector3(0.2f,0.1f,0.2f);
            }      
            if(Time.time-StopButtonFixationZeitpunkt0-StopFeedbackZeit > StopFeedbackIntervall*0.1f && Time.time-StopButtonFixationZeitpunkt0-StopFeedbackZeit < StopFeedbackIntervall*0.2f)
            {
                StopFeedbackButton.transform.localScale = new Vector3(0.4f,0.1f,0.4f);
            }      
            if(Time.time-StopButtonFixationZeitpunkt0-StopFeedbackZeit > StopFeedbackIntervall*0.2f && Time.time-StopButtonFixationZeitpunkt0-StopFeedbackZeit < StopFeedbackIntervall*0.3f)
            {
                StopFeedbackButton.transform.localScale = new Vector3(0.6f,0.1f,0.6f);
            }      
            if(Time.time-StopButtonFixationZeitpunkt0-StopFeedbackZeit > StopFeedbackIntervall*0.3f && Time.time-StopButtonFixationZeitpunkt0-StopFeedbackZeit < StopFeedbackIntervall*0.4f)
            {
                StopFeedbackButton.transform.localScale = new Vector3(0.8f,0.1f,0.8f);
            }      
            if(Time.time-StopButtonFixationZeitpunkt0-StopFeedbackZeit > StopFeedbackIntervall*0.4f && Time.time-StopButtonFixationZeitpunkt0-StopFeedbackZeit < StopFeedbackIntervall*0.5f)
            {
                StopFeedbackButton.transform.localScale = new Vector3(1f,0.1f,1f);
            }      
            if(Time.time-StopButtonFixationZeitpunkt0-StopFeedbackZeit > StopFeedbackIntervall*0.5f && Time.time-StopButtonFixationZeitpunkt0-StopFeedbackZeit < StopFeedbackIntervall*0.6f)
            {
                StopFeedbackButton.transform.localScale = new Vector3(1.2f,0.1f,1.2f);
            }      
            if(Time.time-StopButtonFixationZeitpunkt0-StopFeedbackZeit > StopFeedbackIntervall*0.6f && Time.time-StopButtonFixationZeitpunkt0-StopFeedbackZeit < StopFeedbackIntervall*0.7f)
            {
                StopFeedbackButton.transform.localScale = new Vector3(1.4f,0.1f,1.4f);
            }      
            if(Time.time-StopButtonFixationZeitpunkt0-StopFeedbackZeit > StopFeedbackIntervall*0.7f && Time.time-StopButtonFixationZeitpunkt0-StopFeedbackZeit < StopFeedbackIntervall*0.8f)
            {
                StopFeedbackButton.transform.localScale = new Vector3(1.6f,0.1f,1.6f);
            }      
            if(Time.time-StopButtonFixationZeitpunkt0-StopFeedbackZeit > StopFeedbackIntervall*0.8f && Time.time-StopButtonFixationZeitpunkt0-StopFeedbackZeit < StopFeedbackIntervall*0.9f)
            {
                StopFeedbackButton.transform.localScale = new Vector3(1.8f,0.1f,1.8f);
            }      
            if(Time.time-StopButtonFixationZeitpunkt0-StopFeedbackZeit > StopFeedbackIntervall*0.9f)
            {
                StopFeedbackButton.transform.localScale = new Vector3(2f,0.1f,2f);

            }      
        }
        //Auswahl durch Fixation
        if ((CoreServices.InputSystem.EyeGazeProvider.GazeTarget == StopButton || CoreServices.InputSystem.EyeGazeProvider.GazeTarget == StopFeedbackButton) && Time.time-StopButtonFixationZeitpunkt0 > StopFixationZeit)
        {
            StopButtonFixationZeitpunkt0 = 0;
            event_stop.Invoke();

            //Start Phase 2
            Phase = 2;
            StopButton.SetActive(false);
            StopFeedbackButton.SetActive(false);

            if(MovingObject1.activeSelf){
                MovingObject1.SetActive(false);
                MovingObject1Line.SetActive(false);
            }
            if(MovingObject2.activeSelf){
                MovingObject2.SetActive(false);
                MovingObject2Line.SetActive(false);
            }
            if(MovingObject3.activeSelf){
                MovingObject3.SetActive(false);
                MovingObject3Line.SetActive(false);
            }
            if(MovingObject4.activeSelf){
                MovingObject4.SetActive(false);
                MovingObject4Line.SetActive(false);
            }

            StartCoroutine(Phase2To0());            
        }

    }

    //Nach erfolgreichem Stop warted diese Funktion fuer 1 Sekunde bevor der Start Button wieder aktiviert wird.
    IEnumerator Phase2To0()
    {
        yield return new WaitForSeconds(1);
        StartButton.SetActive(true);
        Phase = 0;

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = CoreServices.InputSystem.EyeGazeProvider.GazeOrigin + CoreServices.InputSystem.EyeGazeProvider.GazeDirection.normalized * 0.5f;

        CheckStartButton();
        CheckStopButton();

        ListenForEventStart();
        ListenForEventStop();
        ListenForEventGeradeaus();
        ListenForEventRueckwaerts();
        ListenForEventRechts();
        ListenForEventLinks();

    }
}
