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

		Vec3 magnetisation;

		Quat orientation;
		Vec3 angularVelocity;
		Vec3 angularAcceleration;

		int battery;

		int temperature;

		int magnetX;
		int magnetY;
		int magnetZ;

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