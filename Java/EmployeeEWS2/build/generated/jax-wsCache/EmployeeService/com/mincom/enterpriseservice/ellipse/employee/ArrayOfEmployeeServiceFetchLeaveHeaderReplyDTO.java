
package com.mincom.enterpriseservice.ellipse.employee;

import java.util.ArrayList;
import java.util.List;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for ArrayOfEmployeeServiceFetchLeaveHeaderReplyDTO complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="ArrayOfEmployeeServiceFetchLeaveHeaderReplyDTO">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="EmployeeServiceFetchLeaveHeaderReplyDTO" type="{http://employee.ellipse.enterpriseservice.mincom.com}EmployeeServiceFetchLeaveHeaderReplyDTO" maxOccurs="unbounded" minOccurs="0"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "ArrayOfEmployeeServiceFetchLeaveHeaderReplyDTO", propOrder = {
    "employeeServiceFetchLeaveHeaderReplyDTO"
})
public class ArrayOfEmployeeServiceFetchLeaveHeaderReplyDTO {

    @XmlElement(name = "EmployeeServiceFetchLeaveHeaderReplyDTO", nillable = true)
    protected List<EmployeeServiceFetchLeaveHeaderReplyDTO> employeeServiceFetchLeaveHeaderReplyDTO;

    /**
     * Gets the value of the employeeServiceFetchLeaveHeaderReplyDTO property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the employeeServiceFetchLeaveHeaderReplyDTO property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getEmployeeServiceFetchLeaveHeaderReplyDTO().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link EmployeeServiceFetchLeaveHeaderReplyDTO }
     * 
     * 
     */
    public List<EmployeeServiceFetchLeaveHeaderReplyDTO> getEmployeeServiceFetchLeaveHeaderReplyDTO() {
        if (employeeServiceFetchLeaveHeaderReplyDTO == null) {
            employeeServiceFetchLeaveHeaderReplyDTO = new ArrayList<EmployeeServiceFetchLeaveHeaderReplyDTO>();
        }
        return this.employeeServiceFetchLeaveHeaderReplyDTO;
    }

}
