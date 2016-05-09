#pragma once

#include "MoveConfig.h"
#include "Quat.h"
#include "Vec3.h"
#include "MoveButton.h"

namespace Move
{
	struct MOVE_EXPORT MoveData
	{
		Vec3 position;
		Vec3 velocity;
		Vec3 acceleration;

		// For Calibrationtool comment out from here ...
		Vec3 magnetometer;
		int battery;
		int temperature;
		// ... to here

		//int magnetX;
		//int magnetY;
		//int magnetZ;

		Quat orientation;
		Vec3 angularVelocity;
		Vec3 angularAcceleration;

		int buttons;
		int trigger;

		MoveData()
		{
			buttons=0;
			trigger=0;
		}

		bool isButtonPressed(MoveButton button)
		{
			return (buttons & button)!=0;
		}
	};
}