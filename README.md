## **SSVEP VR Application**
This Unity project is a VR application designed for Meta Quest devices. The application consists of multiple experiments, each with multiple targets. The experiments are managed by a global object which communicates with a server through TCP.

### Scripts:

1. **TCPClient**: Establishes a TCP connection to a server, sends and receives messages, and handles the connection status.
2. **ExperimentManager**: Manages the experiment flow by setting the active experiment and cycling through experiments. It also sets the OVR refresh rate to 90Hz.
3. **ExperimentData**: Holds the experiment number and manages the visibility and SSVEP status of target objects within each experiment.
4. **SSVEPManager**: Controls the start, pause, and resume of SSVEP components based on elapsed time and communicates with the server.
5. **TargetData**: Manages the visibility of the "cross" object within each target.
6. **SSVEP**: Controls the flickering of target objects (Left and Right) using sine or square wave patterns based on the specified frequency.

### Scene Hierarchy:
```
Global (GameObject)
|-- TCPClient (Script)
|-- ExperimentManager (Script)
|-- Experiment1 (GameObject)
|   |-- ExperimentData (Script)
|   |-- SSVEPManager (Script)
|   |-- Target_0 (GameObject)
|   |   |-- TargetData (Script)
|   |   |-- Left (GameObject)
|   |   |   |-- SSVEP (Script)
|   |   |-- Right (GameObject)
|   |   |   |-- SSVEP (Script)
|   |   |-- Cross (GameObject)
|   |-- (Other Targets with similar structure)
|-- Experiment2 (GameObject)
|   |-- (Same structure as Experiment_0)
|-- ...
```