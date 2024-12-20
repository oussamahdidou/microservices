package com.api.stockservice.domain.IServices;

import com.api.stockservice.application.DTOs.CreateSupplierDto;
import com.api.stockservice.application.DTOs.SupplierDto;
import com.api.stockservice.domain.Entities.Supplier;

import java.util.List;

public interface ISupplierService {
    Supplier createSupllier(SupplierDto supplierDto);
    Supplier GetSupplier(Long ID);
    List<Supplier> GetAllSuppliers();
    Supplier UpdateSupplier(Long ID, SupplierDto supplierDto);
    boolean DeleteSupplier(Long ID);
}
