using EntityFramework.Exceptions.Common;
using Nexium.API.Configuration;
using Nexium.API.Data.Repositories;
using Nexium.API.Entities;
using Nexium.API.Exceptions;
using Nexium.API.TransferObjects.EmployeePosition;

namespace Nexium.API.Services.Implementation;

public class EmployeePositionsService(
    EmployeePositionMapper mapper,
    IEmployeePositionsRepository employeePositionsRepository,
    SelectedLanguageService selectedLanguageService,
    ITranslationMappingRepository translationMappingRepository) : IEmployeePositionsService
{
    public async Task<List<EmployeePositionView>> GetAllEmployeePositions(CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var employeePositions =
            await employeePositionsRepository.GetAllEmployeePositions(cancellationToken);

        var employeePositionViews = new List<EmployeePositionView>();
        foreach (var x in employeePositions)
        {
            var translatedLabel = (await translationMappingRepository.GetSingleEntityTranslationForAttribute(
                "EmployeePosition", x.Id, "Label", selectedLanguage, cancellationToken))?.TranslatedText ?? x.Label;

            employeePositionViews.Add(new EmployeePositionView
            {
                Id = x.Id,
                Label = translatedLabel
            });
        }

        return employeePositionViews;
    }

    public async Task<EmployeePositionView> GetSingleEmployeePositionById(ushort employeePositionId,
        CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var employeePosition =
            await employeePositionsRepository.GetSingleEmployeePositionById(employeePositionId, cancellationToken,
                true);
        if (employeePosition == null)
            throw new EntityNotFoundException(nameof(EmployeePosition), nameof(employeePositionId),
                employeePositionId.ToString());
        return new EmployeePositionView
        {
            Id = employeePosition.Id,
            Label = (await translationMappingRepository.GetSingleEntityTranslationForAttribute("EmployeePosition",
                        employeePosition.Id, "Label", selectedLanguage, cancellationToken))?.TranslatedText ??
                    employeePosition.Label
        };
    }

    public async Task<EmployeePositionView> CreateEmployeePosition(EmployeePositionSave employeePositionToCreate,
        CancellationToken cancellationToken)
    {
        var employeePosition = mapper.MapToEmployeePosition(employeePositionToCreate);
        var createdEmployeePosition =
            await employeePositionsRepository.CreateEmployeePosition(employeePosition, cancellationToken);
        return mapper.MapToEmployeePositionView(createdEmployeePosition);
    }

    public async Task UpdateEmployeePosition(ushort employeePositionId, EmployeePositionSave employeePositionToUpdate,
        CancellationToken cancellationToken)
    {
        var employeePositionUpdatedValues = mapper.MapToEmployeePosition(employeePositionToUpdate);
        var existingEmployeePosition =
            await employeePositionsRepository.GetSingleEmployeePositionById(employeePositionId, cancellationToken);
        if (existingEmployeePosition == null)
            throw new EntityNotFoundException(nameof(EmployeePosition), nameof(employeePositionId),
                employeePositionId.ToString());
        existingEmployeePosition.Label = employeePositionUpdatedValues.Label;
        await employeePositionsRepository.UpdateEmployeePosition(existingEmployeePosition, cancellationToken);
    }

    public async Task DeleteEmployeePosition(ushort employeePositionId, CancellationToken cancellationToken)
    {
        var existingEmployeePosition =
            await employeePositionsRepository.GetSingleEmployeePositionById(employeePositionId, cancellationToken);
        if (existingEmployeePosition == null)
            throw new EntityNotFoundException(nameof(EmployeePosition), nameof(employeePositionId),
                employeePositionId.ToString());
        try
        {
            await employeePositionsRepository.DeleteEmployeePosition(existingEmployeePosition, cancellationToken);
        }
        catch (ReferenceConstraintException)
        {
            throw new EntityReferencedException(nameof(EmployeePosition), nameof(employeePositionId),
                employeePositionId.ToString());
        }
    }
}