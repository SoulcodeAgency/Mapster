﻿using Benchmark.Classes;
using BenchmarkDotNet.Attributes;

namespace Benchmark.Benchmarks
{
    public class TestComplexTypes
    {
        private Customer customerInstance;

        [Params(1000, 10_000, 100_000, 1_000_000)]
        public int Iterations { get; set; }

        [Benchmark]
        public void MapsterTest()
        {
            TestAdaptHelper.TestMapsterAdapter<Customer, CustomerDTO>(customerInstance, Iterations);
        }
        
        [Benchmark(Description = "Mapster 5.0.0 (Roslyn)")]
        public void RoslynTest()
        {
            TestAdaptHelper.TestMapsterAdapter<Customer, CustomerDTO>(customerInstance, Iterations);
        }

        [Benchmark(Description = "Mapster 5.0.0 (FEC)")]
        public void FecTest()
        {
            TestAdaptHelper.TestMapsterAdapter<Customer, CustomerDTO>(customerInstance, Iterations);
        }

        [Benchmark]
        public void CodegenTest()
        {
            TestAdaptHelper.TestCodeGen(customerInstance, Iterations);
        }

        [Benchmark]
        public void ExpressMapperTest()
        {
            TestAdaptHelper.TestExpressMapper<Customer, CustomerDTO>(customerInstance, Iterations);
        }

        [Benchmark]
        public void AutoMapperTest()
        {
            TestAdaptHelper.TestAutoMapper<Customer, CustomerDTO>(customerInstance, Iterations);
        }

        [GlobalSetup(Target = nameof(MapsterTest))]
        public void SetupMapster()
        {
            customerInstance = TestAdaptHelper.SetupCustomerInstance();
            TestAdaptHelper.ConfigureMapster(customerInstance, MapsterCompilerType.Default);
        }

        [GlobalSetup(Target = nameof(RoslynTest))]
        public void SetupRoslyn()
        {
            customerInstance = TestAdaptHelper.SetupCustomerInstance();
            TestAdaptHelper.ConfigureMapster(customerInstance, MapsterCompilerType.Roslyn);
        }

        [GlobalSetup(Target = nameof(FecTest))]
        public void SetupFec()
        {
            customerInstance = TestAdaptHelper.SetupCustomerInstance();
            TestAdaptHelper.ConfigureMapster(customerInstance, MapsterCompilerType.FEC);
        }

        [GlobalSetup(Target = nameof(CodegenTest))]
        public void SetupCodegen()
        {
            customerInstance = TestAdaptHelper.SetupCustomerInstance();
            CustomerMapper.Map(customerInstance);
        }

        [GlobalSetup(Target = nameof(ExpressMapperTest))]
        public void SetupExpressMapper()
        {
            customerInstance = TestAdaptHelper.SetupCustomerInstance();
            TestAdaptHelper.ConfigureExpressMapper(customerInstance);
        }

        [GlobalSetup(Target = nameof(AutoMapperTest))]
        public void SetupAutoMapper()
        {
            customerInstance = TestAdaptHelper.SetupCustomerInstance();
            TestAdaptHelper.ConfigureAutoMapper(customerInstance);
        }
    }
}