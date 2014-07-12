﻿using System;
using AcspNet.Meta;
using AcspNet.Tests.TestEntities;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests.Meta
{
	[TestFixture]
	public class ControllersMetaStoreTests
	{
		[Test]
		public void GetControllersMetaData_LocalControllers_GetAndSortedCorrectly()
		{
			// Assign

			AcspTypesFinder.ExcludedAssembliesPrefixes.Remove("AcspNet");
			AcspTypesFinder.CleanLoadedTypesAndAssenbliesInfo();

			var factory = new Mock<IControllerMetaDataFactory>();
			var store = new ControllersMetaStore(factory.Object);

			factory.SetupSequence(x => x.CreateControllerMetaData(It.IsAny<Type>()))
				.Returns(new ControllerMetaData(typeof (TestController2), new ControllerExecParameters(null, 2)))
				.Returns(new ControllerMetaData(typeof (TestController1), new ControllerExecParameters(null, 1)));

			// Act
			var metaData = store.GetControllersMetaData();

			Assert.AreEqual(2, metaData.Count);
			Assert.AreEqual("TestController1", metaData[0].ControllerType.Name);
			Assert.AreEqual("TestController2", metaData[1].ControllerType.Name);

			factory.Verify(x => x.CreateControllerMetaData(It.Is<Type>(t => t == typeof(TestController1))));
			factory.Verify(x => x.CreateControllerMetaData(It.Is<Type>(t => t == typeof(TestController2))));
		}
	}
}