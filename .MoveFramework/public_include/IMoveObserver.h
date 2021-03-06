#pragma once

#include "MoveConfig.h"
#include "MoveData.h"
#include "MoveButton.h"
#include "NavData.h"

namespace Move
{
	enum MoveMessage
	{
		M_InitialCalibratingDone,
		M_Calibrated,

		M_RotateMove,
		M_CalibrationProcessing,
		M_CantReadCalibration,

		M_UseMagnetometers,
		M_DoesntUseMagnetometers,
	};

	class MOVE_EXPORT IMoveObserver
	{
	public:
		virtual void moveUpdated(int moveId, MoveData data){}
		virtual void moveKeyPressed(int moveId, MoveButton button){}
		virtual void moveKeyReleased(int moveId, MoveButton button){}
		virtual void moveConnected(int numAllMoves){}
		virtual void moveNotify(int moveId, MoveMessage message){}

		virtual void navUpdated(int navId, NavData data){}
		virtual void navKeyPressed(int navId, MoveButton button){}
		virtual void navKeyReleased(int navId, MoveButton button){}
	};
}