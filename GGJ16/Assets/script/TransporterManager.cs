using UnityEngine;

public class TransporterManager : MonoBehaviour {

	[Header("Transporters")]
	public Transporter hallway;
	public Transporter messHall;
	public Transporter aiBay;
	public Transporter cargoBay;
	public Transporter engineRoom;
	public Transporter medBay;

	public void hallwayToCargo() {
		ChangeTransporterDestination(hallway, cargoBay, "Cargo Bay");
	}

	public void hallwayToMess() {
		ChangeTransporterDestination(hallway, messHall, "Mess Hall");
	}

	public void hallwayToAI() {
		ChangeTransporterDestination(hallway, aiBay, "AI Core");
	}

	public void messToHallway() {
		ChangeTransporterDestination(messHall, hallway, "Hallway");
	}

	public void messToMedbay() {
		ChangeTransporterDestination(messHall, medBay, "Medbay");
	}

	public void cargoToHallway() {
		ChangeTransporterDestination(cargoBay, hallway, "Hallway");
	}

	public void cargoToEngine() {
		ChangeTransporterDestination(cargoBay, engineRoom, "Engine Room");
	}

	public void engineToCargo() {
		ChangeTransporterDestination(engineRoom, cargoBay, "Cargo Bay");
	}

	void ChangeTransporterDestination(Transporter a, Transporter b, string roomName) {
		a.ChangeDestination(b.transform, "transport to " + roomName);
	}

}